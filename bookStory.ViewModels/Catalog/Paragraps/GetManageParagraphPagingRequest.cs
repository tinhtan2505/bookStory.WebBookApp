using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Paragraps
{
    public class GetManageParagraphPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public int? IdBook { get; set; }
    }
}