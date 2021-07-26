using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Books
{
    public class GetPublicBookPagingRequest : PagingRequestBase
    {
        public int? IdProject { set; get; }
    }
}