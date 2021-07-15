using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Chats
{
    public class GetManageChatPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        //public string? Message { get; set; }
    }
}