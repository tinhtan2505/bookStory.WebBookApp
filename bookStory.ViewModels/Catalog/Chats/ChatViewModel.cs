using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Chats
{
    public class ChatViewModel
    {
        public int Id { set; get; }
        public Guid UserIdSender { set; get; }
        public Guid UserIdReceiver { set; get; }
        public string Message { set; get; }
        public DateTime DateComment { get; set; }
        public string FirstName1 { get; set; }
        public string LastName1 { get; set; }
        public string UserName1 { get; set; }
        public string FirstName2 { get; set; }
        public string LastName2 { get; set; }
        public string UserName2 { get; set; }
    }
}