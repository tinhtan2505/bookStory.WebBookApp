using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Books
{
    public class GetManageBookPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public string LanguageId { set; get; }
        //public List<int> IdProject { get; set; }
    }
}