using bookStory.ViewModels.Catalog.Books;
using bookStory.ViewModels.Utilities.Slides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStory.WebBookApp.Models
{
    public class HomeViewModel
    {
        public List<SlideVm> Slides { get; set; }

        public List<BookViewModel> FeaturedProducts { get; set; }

        public List<BookViewModel> LatestProducts { get; set; }
    }
}