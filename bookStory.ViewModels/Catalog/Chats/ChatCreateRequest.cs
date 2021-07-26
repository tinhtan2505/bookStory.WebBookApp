using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Chats
{
    public class ChatCreateRequest
    {
        public Guid UserIdSender { set; get; }
        public Guid UserIdReceiver { set; get; }
        public string Message { set; get; }
    }
}