using bookStory.ViewModels.Catalog.Books;
using bookStory.ViewModels.System.Users;
using bookStory.ViewModels.Utilities.Slides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStory.WebBookApp.Models
{
    public class HomeViewModel
    {
        public List<UserVm> TopUsers { get; set; }

        public List<BookViewModel> FeaturedProducts { get; set; }

        public List<BookViewModel> LatestProducts { get; set; }
    }
}