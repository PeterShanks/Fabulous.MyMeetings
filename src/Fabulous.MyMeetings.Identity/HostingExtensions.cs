using Duende.AccessTokenManagement;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using Fabulous.MyMeetings.Identity.UserManagement;
using Serilog;
using Scope = Fabulous.MyMeetings.Scopes.Scope;

namespace Fabulous.MyMeetings.Identity;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddRazorPages();

        services.AddIdentityServer(options =>
            {
                options.EmitScopesAsSpaceDelimitedStringInJwt = true;
            })
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryClients(Config.Clients);

        services.AddScoped<UserManagementService>();
        services.Configure<UserManagementServiceSettings>(configuration.GetSection("UserManagement"));

        services.AddClientCredentialsTokenManagement()
            .AddClient("user-access.api", client =>
            {
                client.TokenEndpoint = new Uri($"{configuration["IdentityServer:HostUrl"]}/connect/token");
                client.ClientId = ClientId.Parse(configuration["IdentityServer:ClientId"]!);
                client.ClientSecret = ClientSecret.Parse(configuration["IdentityServer:Secret"]!);
                client.Scope = Duende.AccessTokenManagement.Scope.Parse($"{Scope.User.Authenticate} {Scope.User.Read}");
            });

        services.AddHttpClient<UserManagementService>(client =>
        {
            client.BaseAddress =
                new Uri(configuration["UserManagement:BaseUrl"] ?? throw new InvalidOperationException());
        })
        .AddClientCredentialsTokenHandler(ClientCredentialsClientName.Parse("user-access.api"));

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