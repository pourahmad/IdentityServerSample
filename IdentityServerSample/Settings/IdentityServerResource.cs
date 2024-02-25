using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace IdentityServerSample.Settings
{
    public class IdentityServerResource
    {
        public static List<TestUser> GetTestUsers => new List<TestUser>()
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "nasser",
                    Password = "Nasser@1234",
                    Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, "nasser@gmail.com"),
                        new Claim(ClaimTypes.MobilePhone, "093500000000"),
                        new Claim("fullName", "nasser pourahmad"),
                        new Claim("permissions", "permit1, permit2, permit3, permit4"),
                    }
                }
            };

        public static List<IdentityResource> GetIdentityResources => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResource("fullName", new List<string>{ "fullName"}),
                new IdentityResource("permissions", new List<string>{ "permissions"}),
            };

        public static List<Client> GetClients => new List<Client>
            {
                new Client
                {
                    ClientId = "efe06a2e-2081-4362-a966-be477dde2ac6",
                    ClientName = "ClientAspNetCoreMVC",
                    ClientSecrets = {new Secret("Sample@123456".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = {"https://localhost:7047/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:5002/signout-callback-oidc"},
                    AllowedScopes = GetStandardScopes
                },
                new Client
                {
                    ClientId = "bb9f475f-e73d-47dc-85ba-63695ed0a2ac",
                    ClientSecrets = {new Secret("SampleAPI@123456".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = new[] { "ClientAspNetCoreWebAPI" }
                }
            };

        private static List<string> GetStandardScopes => new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.Phone,
                IdentityServerConstants.StandardScopes.Address,
                {"fullName" },
                {"permissions" }
            };

        public static List<ApiResource> GetApiResources => new List<ApiResource>
        {
            new ApiResource("ClientAspNetCoreWebAPI","سرویس تستی API")
        };

        public static List<ApiScope> GetApiScopes => new List<ApiScope>
        {
            new ApiScope("ClientAspNetCoreWebAPI", "سرویس تستی API")
        };
    }
}
