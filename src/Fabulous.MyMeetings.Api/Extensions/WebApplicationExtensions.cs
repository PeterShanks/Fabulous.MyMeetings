using Fabulous.MyMeetings.Api.Configuration.ExecutionContext;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Fabulous.MyMeetings.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> Configure(this WebApplicationBuilder webAppBuilder)
        {
            var app = webAppBuilder.Build();

            await app.InitializeModules();

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

        private static async Task<WebApplication> InitializeModules(this WebApplication app)
        {
            var configuration = app.Configuration;
            var serviceProvider = app.Services;

            await UserAccessStartup.Initialize(
                configuration,
                configuration.GetConnectionString("MyMeetings")!,
                new ExecutionContextAccessor(serviceProvider.GetRequiredService<IHttpContextAccessor>()),
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                serviceProvider.GetRequiredService<IConfigureOptions<LoggerFilterOptions>>(),
                new EmailsConfiguration() { FromEmail = configuration["EmailsConfiguration:FromEmail"]! },
                null,
                null
            );

            return app;
        }
    }
}
