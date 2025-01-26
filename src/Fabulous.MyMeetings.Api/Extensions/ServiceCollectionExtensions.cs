using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
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
using Fabulous.MyMeetings.Api.Configuration.Validation;
using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Hellang.Middleware.ProblemDetails;

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

        services.AddSingleton<IAuthorizationMiddlewareResultHandler, ProblemDetailsAuthorizationMiddlewareResultHandler>();

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
                Type = SecuritySchemeType.ApiKey,
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "oauth2"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }
}