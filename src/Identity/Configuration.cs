using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace Identity
{
    public static class Configuration
    {
        public static IEnumerable<ApiResource> GetApiResources()
             => new List<ApiResource>
            {
                new ApiResource("CollectionApi") {Scopes = new List<string>() {  "CollectionApi" }},
            };

        public static IEnumerable<Client> GetClients()
            => new List<Client>
            {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = {new Secret("client_secret".ToSha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = 
                    {
                        "CollectionApi"
                    }  
                },
                new Client
                {
                    ClientId = "client_web_id",
                    ClientSecrets = {new Secret("client_secret_web".ToSha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = 
                    {
                        "CollectionApi",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    RequireConsent = false,
                    RedirectUris = {"http://web/signin-oidc"},
                    PostLogoutRedirectUris = { "http://web/signout-callback-oidc" },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,
                    AllowedCorsOrigins = {"http:/web"}
                    
                    
                    
                }
            };
      

        public static IEnumerable<IdentityResource> GetIdentityResources()
            => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> GetApiScopes()
            => new List<ApiScope>
                {
                    new ApiScope("CollectionApi")
                };
        
    }
}