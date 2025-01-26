using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using Fabulous.MyMeetings.Identity.UserManagement;
using Fabulous.MyMeetings.Scopes;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Fabulous.MyMeetings.Identity;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

        services.AddRazorPages();

        services.AddIdentityServer()
            .AddConfigurationStore(opts =>
            {
                opts.ConfigureDbContext = b => b
                    .UseSqlServer(
                        configuration.GetConnectionString("IdentityServerDb"),
                        sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(opts =>
            {
                opts.ConfigureDbContext = b => b
                    .UseSqlServer(configuration.GetConnectionString("IdentityServerDb"),
                        sql => sql.MigrationsAssembly(migrationsAssembly));

                opts.EnableTokenCleanup = true;
            })
            .AddConfigurationStoreCache();

        services.AddScoped<UserManagementService>();
        services.Configure<UserManagementServiceSettings>(configuration.GetSection("UserManagement"));

        services.AddClientCredentialsTokenManagement()
            .AddClient("user-access.api", client =>
            {
                client.TokenEndpoint = $"{configuration["IdentityServer:HostUrl"]}/connect/token";
                client.ClientId = configuration["IdentityServer:ClientId"];
                client.ClientSecret = configuration["IdentityServer:Secret"];
                client.Scope = Scope.User.Authenticate;
            });

        services.AddHttpClient<UserManagementService>(client =>
        {
            client.BaseAddress =
                new Uri(configuration["UserManagement:BaseUrl"] ?? throw new InvalidOperationException());
        })
        .AddClientCredentialsTokenHandler("user-access.api");

        services.AddTransient<IResourceOwnerPasswordValidator, UserManagementResourceOwnerPasswordValidator>();
        services.AddScoped<IProfileService, UserManagementProfileService>();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseStaticFiles();
        app.UseRouting();

        app.UseIdentityServer();

        app.UseAuthorization();
        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}