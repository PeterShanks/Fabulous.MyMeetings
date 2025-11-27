using System.Reflection;
using Serilog;
using Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;
using Fabulous.MyMeetings.Api.Configuration.Authorization.Scope;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;
using Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Fabulous.MyMeetings.Api.Configuration;
using Fabulous.MyMeetings.Api.Configuration.Middlewares;
using Fabulous.MyMeetings.Api.Configuration.ProblemDetailsMapping;
using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Hellang.Middleware.ProblemDetails;
using Fabulous.MyMeetings.BuildingBlocks.Application.Exceptions;
using Fabulous.MyMeetings.CommonServices.Email.MailKit;
using Fabulous.MyMeetings.Modules.Administration.Application.Contracts;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure;
using Fabulous.MyMeetings.Modules.Meetings.Application.Contracts;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens;
using Microsoft.OpenApi;

namespace Fabulous.MyMeetings.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddSwagger();
        services.AddHostedService<ModuleBackgroundServices>();
        services.AddSerilog(cfg => cfg
            .Enrich.With<ModuleEnricher>()
            .ReadFrom.Configuration(builder.Configuration)
        );
        services.AddProblemDetails(opts =>
        {
            opts.IncludeExceptionDetails = (context, exception) => builder.Environment.IsDevelopment();
            opts.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
            opts.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            opts.Map<BusinessException>(ex => new BusinessExceptionProblemDetails(ex));
            opts.Map<NotFoundException>(ex => new NotFoundProblemDetails(ex));
            opts.Map<InvalidTokenException>(_ => new InvalidTokenProblemDetails());
            opts.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                builder.Configuration.Bind("JwtSettings", options);
                options.TokenValidationParameters.ValidateAudience = false;
                options.MapInboundClaims = false;
                options.TokenValidationParameters.ValidTypes = ["at+jwt"];
            });
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy(HasScopeAttribute.HasScopePolicyName, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new HasScopeAuthorizationRequirement());
            });

            options.AddPolicy(HasPermissionAttribute.HasPermissionPolicyName, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new HasPermissionAuthorizationRequirement());
            });
        });

        services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();
        services.AddScoped<IAuthorizationHandler, HasScopeAuthorizationHandler>();

        services.AddScoped<IUserAccessModule, UserAccessModule>();
        services.AddScoped<IUserRegistrationsModule, UserRegistrationsModule>();
        services.AddScoped<IAdministrationModule, AdministrationModule>();
        services.AddScoped<IMeetingsModule, MeetingsModule>();

        services.AddSingleton<IAuthorizationMiddlewareResultHandler, ProblemDetailsAuthorizationMiddlewareResultHandler>();

        services.AddMailKit(settings => builder.Configuration.GetSection("SmtpSettings").Bind(settings));
        services.AddOptionsWithValidation<EmailsConfiguration, EmailsConfigurationValidator>(builder.Configuration.GetSection("EmailsConfiguration"));

        services.AddSingleton<IEventBus, InMemoryEventBus>();

        return builder;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "MyMeetings API",
                Description = "MyMeetings API for modular monolith .NET application."
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(openApiDocument => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", openApiDocument)] = []
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }
}