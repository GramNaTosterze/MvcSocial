using System.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MvcSocial.Models;

namespace MvcSocial.Controllers
{
    public class HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IStringLocalizer<HomeController> _localizer = localizer;


        public IActionResult Index()
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
        
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Status(int code)
        {
            ViewData["code"] = code;
            ViewData["message"] = code switch
            {
                403 => _localizer["c403"],
                404 => _localizer["c404"],
                _ => _localizer["default"]
            };
            return View();
        }
    }
}
