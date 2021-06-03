using bookStory.ViewModels.Catalog.Books;
using bookStory.ViewModels.Catalog.Paragraps;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStory.WebBookApp.Models
{
    public class BookDetailViewModel
    {
        public BookViewModel Book { get; set; }
        public PagedResult<ParagraphViewModel> ListParagraphs { get; set; }
    }
}