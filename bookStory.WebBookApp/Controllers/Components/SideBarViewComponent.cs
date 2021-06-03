using bookStory.ApiIntegration;
using bookStory.ApiIntegration.Book;
using bookStory.ApiIntegration.Project;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace bookStory.WebApp.Controllers.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly IBookApiClient _bookApiClient;

        public SideBarViewComponent(IBookApiClient bookApiClient)
        {
            _bookApiClient = bookApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _bookApiClient.GetAll();
            return View(items);
        }
    }
}