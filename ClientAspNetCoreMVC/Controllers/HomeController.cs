using ClientAspNetCoreMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ClientAspNetCoreMVC.Token;
using IdentityModel.Client;

namespace ClientAspNetCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Tokens token = new Tokens();
            TokenResponse accessToken = token.GetToken();

            HttpClient apiClient = new HttpClient();
            apiClient.SetBearerToken(accessToken.AccessToken);
            var result = apiClient.GetFromJsonAsync<List<UserDto>>("https://localhost:7048/values/GetUsers").Result;
            return View(result);
        }

        [Authorize]
        public IActionResult UserPanel()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
