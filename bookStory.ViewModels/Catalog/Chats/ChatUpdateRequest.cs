using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Chats
{
    public class ChatUpdateRequest
    {
        public int Id { set; get; }
        public Guid UserIdSender { set; get; }
        public Guid UserIdReceiver { set; get; }
        public string Message { set; get; }
        public DateTime DateComment { get; set; }
    }
}