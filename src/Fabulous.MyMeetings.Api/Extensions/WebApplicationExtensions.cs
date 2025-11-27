using Fabulous.MyMeetings.Api.Configuration.ExecutionContext;
using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration;
using Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration;
using Hellang.Middleware.ProblemDetails;
using Microsoft.OpenApi;

namespace Fabulous.MyMeetings.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigurePipeline(this WebApplicationBuilder webAppBuilder)
    {
        var app = webAppBuilder.Build();

        app.InitializeModules();

        app.UseHttpsRedirection();

        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());

        app.UseMiddleware<CorrelationMiddleware>();

        if (!app.Environment.IsDevelopment())
            app.UseHsts();

        app.UseProblemDetails();

        app.UseSwaggerDocumentation();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    private static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
        app.UseSwagger(opts =>
        {
            opts.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1;
        });
        app.UseSwaggerUI(opts =>
        {
            opts.SwaggerEndpoint("/swagger/v1/swagger.json", "MyMeetings API");
            opts.RoutePrefix = string.Empty;
        });

        return app;
    }

    private static WebApplication InitializeModules(this WebApplication app)
    {
        var configuration = app.Configuration;
        var serviceProvider = app.Services;

        var siteSettings = configuration
            .GetSection("SiteSettings")
            .GetWithValidation<SiteSettings, SiteSettingsValidator>();

        var connectionString = configuration.GetConnectionString("MyMeetings");
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        var executionContextAccessor = new ExecutionContextAccessor(serviceProvider.GetRequiredService<IHttpContextAccessor>());
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var hostApplicationLifetime = serviceProvider.GetRequiredService<IHostApplicationLifetime>();
        var emailService = serviceProvider.GetRequiredService<IEmailService>();
        var eventBus = serviceProvider.GetRequiredService<IEventBus>();

        UserAccessStartup.Initialize(
            connectionString,
            executionContextAccessor,
            loggerFactory,
            hostApplicationLifetime,
            emailService,
            eventBus
        );

        UserRegistrationsStartup.Initialize(
            connectionString,
            executionContextAccessor,
            loggerFactory,
            hostApplicationLifetime,
            siteSettings,
            emailService,
            eventBus
        );

        AdministrationStartup.Initialize(
            connectionString,
            executionContextAccessor,
            loggerFactory,
            hostApplicationLifetime,
            eventBus);

        MeetingsStartup.Initialize(
            connectionString,
            executionContextAccessor,
            loggerFactory,
            hostApplicationLifetime,
            siteSettings,
            emailService,
            eventBus);

        return app;
    }
}