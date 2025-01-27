using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Fabulous.MyMeetings.Scopes;

namespace Fabulous.MyMeetings.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new("permissions", "User Permissions", ["permissions"])
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new(Scope.User.Read, [
            Constants.CustomClaims.Permissions
        ]),
        new(Scope.User.Write, [
            Constants.CustomClaims.Permissions
        ]),
        new(Scope.User.Authenticate)
    ];

    public static IEnumerable<Client> Clients =>
    [
        new ()
        {
            ClientName = "Identity Server",
            ClientId = "95a1971b-4ec5-45f6-a57d-85dafafba8eb",
            ClientSecrets = [new Secret("be88b96b-9c47-415b-af14-72adb9f39691".Sha512())],
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes =
            [
                Scope.User.Authenticate,
                Scope.User.Read,
            ]
        },
        new ()
        {
            ClientName = "Fabulous API",
            ClientId = "cdeafbc5-52ed-4faa-99ea-e593043be89c",
            ClientSecrets = [new Secret("1c0eae36-9da1-4447-a1e7-8ef59bc2aede".Sha512())],
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = []
        },
        new()
        {
            ClientName = "Fabulous Web",
            ClientId = "6d7163de-af9d-4b5a-8624-afb2d8215778",
            ClientSecrets = [new Secret("vNfOrJkJIh7bsWkNO2qSXKg0Kwo00QLC".Sha512())],
            AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
            AllowedScopes =
            [
                Scope.User.Read,
                Scope.User.Write,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email
            ],
            RedirectUris = { "https://localhost:5002/signin-oidc" },

            // where to redirect to after logout
            PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
        }
    ];
}