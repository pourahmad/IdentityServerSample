using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityServerSample.Settings;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddTestUsers(IdentityServerResource.GetTestUsers)
    .AddInMemoryIdentityResources(IdentityServerResource.GetIdentityResources)
    .AddInMemoryApiResources(IdentityServerResource.GetApiResources)
    .AddInMemoryClients(IdentityServerResource.GetClients)
    .AddInMemoryApiScopes(IdentityServerResource.GetApiScopes)
    ;

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
