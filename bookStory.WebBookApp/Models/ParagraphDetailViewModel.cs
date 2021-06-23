using bookStory.ViewModels.Catalog.Books;
using bookStory.ViewModels.Catalog.Comments;
using bookStory.ViewModels.Catalog.Paragraps;
using bookStory.ViewModels.Catalog.Translations;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStory.WebBookApp.Models
{
    public class ParagraphDetailViewModel
    {
        public Guid UserIdLoged { set; get; }
        public Guid UserId { set; get; }
        public string UserName { get; set; }
        public int IdTranslation { set; get; }
        public int IdComment { set; get; }
        public string Message { set; get; }
        public string MessageCreate { set; get; }
        public ParagraphViewModel Paragraph { get; set; }
        public BookViewModel Book { get; set; }
        public TranslationViewModel Translation { get; set; }
        public PagedResult<TranslationViewModel> ListTranslations { get; set; }
        public List<CommentViewModel> ListComments { get; set; }
        public int IdParagraph { set; get; }
        public string Text { set; get; }
        public int Rating { set; get; }
        public int Vote { get; set; }
    }
}