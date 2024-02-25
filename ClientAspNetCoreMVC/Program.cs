using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(option =>
{
    option.DefaultScheme = "Cookies";
    option.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", option =>
{
    option.Authority = "https://localhost:7051/";
    option.ClientId = "efe06a2e-2081-4362-a966-be477dde2ac6";
    option.GetClaimsFromUserInfoEndpoint = true;
    option.SignInScheme = "Cookies";
    option.Scope.Add("email");
    option.Scope.Add("phone");
    option.Scope.Add("profile");
    option.Scope.Add("fullName");
    option.Scope.Add("permissions");
    
    option.MapInboundClaims = false; // Don't rename claim types

    option.SaveTokens = true;
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
