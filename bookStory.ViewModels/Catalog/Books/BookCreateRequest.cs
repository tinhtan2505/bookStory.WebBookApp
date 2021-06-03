using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Books
{
    public class BookCreateRequest
    {
        public string FileName { set; get; }
        public string Title { set; get; }
        public string Author { set; get; }
        public bool? IsFeatured { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}