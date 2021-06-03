using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStory.ViewModels.System.Languages;
using Microsoft.AspNetCore.Mvc;

namespace bookStory.AdminApp.Models
{
    public class NavigationViewModel
    {
        public List<LanguageVm> Languages { get; set; }

        public string CurrentLanguageId { get; set; }
    }
}