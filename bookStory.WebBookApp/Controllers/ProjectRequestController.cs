using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStory.WebBookApp.Controllers
{
    public class ProjectRequestController : Controller
    {
        public IActionResult Request()
        {
            return View();
        }
    }
}