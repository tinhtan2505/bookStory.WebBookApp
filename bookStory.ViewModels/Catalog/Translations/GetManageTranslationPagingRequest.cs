using bookStory.ViewModels.Catalog.Comments;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Translations
{
    public class GetManageTranslationPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public int? IdParagraph { get; set; }
        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}