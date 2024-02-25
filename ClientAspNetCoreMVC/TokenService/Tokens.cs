using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.Diagnostics;
namespace ClientAspNetCoreMVC.Token
{
    public class Tokens
    {
        public TokenResponse GetToken()
        {
            HttpClient httpClient = new HttpClient();
            var descoveryDocument = httpClient.GetDiscoveryDocumentAsync("https://localhost:7051").Result;
            if (descoveryDocument.IsError)
            {
                return null;
            }
            var accessToken = httpClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = descoveryDocument.TokenEndpoint,
                    ClientId = "bb9f475f-e73d-47dc-85ba-63695ed0a2ac",
                    ClientSecret = "SampleAPI@123456", 
                    Scope = "ClientAspNetCoreWebAPI"
                }).Result;

            return accessToken;
        }
    }
}
