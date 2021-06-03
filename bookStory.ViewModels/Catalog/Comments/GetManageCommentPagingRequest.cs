using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Comments
{
    public class GetManageCommentPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public int? IdTranslation { get; set; }
    }
}