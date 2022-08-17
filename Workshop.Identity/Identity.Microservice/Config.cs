using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace Identity.Microservice
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] {new ApiScope("ConfirmTransactions"), new ApiScope("Users"), new ApiScope("Items"), new ApiScope("Transactions")};

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("ConfirmTransactions")
                {
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "role" }
                },
                new ApiResource("Users")
                {
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "role" }
                },
                new ApiResource("Items")
                {
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "role" }
                },
                new ApiResource("Transactions")
                {
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "role" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                //m2m client credentials flow client
                new Client
                {
                  ClientId = "m2m.client",
                  ClientName = "Client Credentials Client",

                  AllowedGrantTypes = GrantTypes.ClientCredentials,
                  ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                  AllowedScopes = { "ConfirmTransactions", "Users", "Items", "Transactions" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                  ClientId = "interactive",
                  ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                  AllowedGrantTypes = GrantTypes.Code,

                  RedirectUris = {"https://localhost:5444/signin-oidc"},
                  FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                  PostLogoutRedirectUris = {"https://localhost:5444/signout-callback-oidc"},

                  AllowOfflineAccess = true,
                  AllowedScopes = {"openid", "profile", "ConfirmTransactions"},
                  RequirePkce = true,
                  RequireConsent = true,
                  AllowPlainTextPkce = false
                },
            };
    }
}
