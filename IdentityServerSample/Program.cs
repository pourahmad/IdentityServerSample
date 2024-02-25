using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddTestUsers(new List<TestUser>()
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
                new Claim(ClaimTypes.Role, "User")
            }
        }
    })
    .AddInMemoryIdentityResources(new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Phone(),
        new IdentityResources.Address(),
        new IdentityResources.Email(),
        new IdentityResource("fullName", new List<string>{ "fullName"}),
        new IdentityResource("role", new List<string>{ "role"})
    })
    .AddInMemoryApiResources(new List<ApiResource>{})
    .AddInMemoryClients(new List<Client>
    {
        new Client
        {
            ClientId = "efe06a2e-2081-4362-a966-be477dde2ac6",
            ClientName = "ClientAspNetCoreMVC",
            ClientSecrets = {new Secret("Sample@123456".Sha256()) },
            AllowedGrantTypes = GrantTypes.Implicit,
            RedirectUris = {"https://localhost:5002/signin-oidc"},
            PostLogoutRedirectUris = {"https://localhost:5002/signout-callback-oidc"},
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.Phone,
                IdentityServerConstants.StandardScopes.Address,
                {"role" },
                {"fullName" },
            }
        }
    });

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
