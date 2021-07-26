using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using bookStory.WebBookApp.Models;
using LazZiya.ExpressLocalization;
using bookStory.ApiIntegration.Book;
using System.Globalization;
using bookStory.Utilities.Constants;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using bookStory.ApiIntegration.User;

namespace bookStory.WebBookApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IBookApiClient _bookApiClient;
        private readonly IUserApiClient _userApiClient;

        public HomeController(ILogger<HomeController> logger,
            IBookApiClient bookApiClient,
            IUserApiClient userApiClient)
        {
            _logger = logger;
            _bookApiClient = bookApiClient;
            _userApiClient = userApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentCulture.Name;
            var data = await _userApiClient.GetAll();
            var viewModel = new HomeViewModel
            {
                TopUsers = await _userApiClient.GetAll(),
                FeaturedProducts = await _bookApiClient.GetTops(SystemConstants.ProductSettings.NumberOfFeaturedProducts),
                LatestProducts = await _bookApiClient.GetLatestProducts(SystemConstants.ProductSettings.NumberOfLatestProducts),
            };
            return View(viewModel);
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

        public IActionResult SetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }
    }
}