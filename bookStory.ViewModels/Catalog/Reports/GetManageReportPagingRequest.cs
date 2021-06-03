using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Reports
{
    public class GetManageReportPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}