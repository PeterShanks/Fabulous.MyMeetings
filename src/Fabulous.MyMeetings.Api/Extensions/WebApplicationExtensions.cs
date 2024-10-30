using Fabulous.MyMeetings.Api.Configuration.ExecutionContext;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration;

namespace Fabulous.MyMeetings.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication Configure(this WebApplicationBuilder webAppBuilder)
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

        app.UseSwaggerDocumentation();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    private static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
        app.UseSwagger();
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

        UserAccessStartup.Initialize(
            configuration.GetConnectionString("MyMeetings")!,
            new ExecutionContextAccessor(serviceProvider.GetRequiredService<IHttpContextAccessor>()),
            serviceProvider.GetRequiredService<ILoggerFactory>(),
            new EmailsConfiguration { FromEmail = configuration["EmailsConfiguration:FromEmail"]! },
            serviceProvider.GetRequiredService<IHostApplicationLifetime>()
        );

        RegistrationsStartup.Initialize(
            configuration.GetConnectionString("MyMeetings")!,
            new ExecutionContextAccessor(serviceProvider.GetRequiredService<IHttpContextAccessor>()),
            serviceProvider.GetRequiredService<ILoggerFactory>(),
            new EmailsConfiguration { FromEmail = configuration["EmailsConfiguration:FromEmail"]! },
            serviceProvider.GetRequiredService<IHostApplicationLifetime>()
        );

        return app;
    }
}