using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Data.Entities
{
    public class Chat
    {
        public int Id { set; get; }
        public Guid UserIdSender { set; get; }
        public Guid UserIdReceiver { set; get; }
        public string Message { set; get; }
        public DateTime DateComment { get; set; }
    }
}