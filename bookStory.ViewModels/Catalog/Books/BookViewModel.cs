using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Books
{
    public class BookViewModel
    {
        public int Id { set; get; }
        public string FileName { set; get; }
        public string Title { set; get; }
        public string Author { set; get; } //Tác giả
        public bool? IsFeatured { get; set; }
        public string ThumbnailImage { get; set; }
    }
}