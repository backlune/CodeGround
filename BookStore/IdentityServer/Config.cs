using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope(name: "productApi", displayName:"Product API")
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            //new Client()
            //{
            //    ClientId = "apiClient",
            //    ClientSecrets = { new Secret("secret".Sha256())},

            //    AllowedGrantTypes = GrantTypes.ClientCredentials,
            //    // scopes that client has access to
            //    AllowedScopes = { "api1" }
            //},
            // interactive ASP.NET Core Web App
            //new Client
            //{
            //    ClientId = "web",
            //    ClientSecrets = { new Secret("secret".Sha256()) },

            //    AllowedGrantTypes = GrantTypes.Code,
            
            //    // where to redirect to after login
            //    RedirectUris = { "https://localhost:7159/signin-oidc" },

            //    // where to redirect to after logout
            //    PostLogoutRedirectUris = { "https://localhost:7159/signout-callback-oidc" },

            //    AllowedScopes =
            //    {
            //        IdentityServerConstants.StandardScopes.OpenId,
            //        IdentityServerConstants.StandardScopes.Profile
            //    }
            //},
            new Client
            {
                ClientId = "bff",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
    
                // where to redirect to after login
                RedirectUris = { "https://localhost:7159/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:7159/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "productApi"
                },
                Claims = new List<ClientClaim>()
                {
                    new ClientClaim("role", "admin")
                    
                }
            }

        };
}