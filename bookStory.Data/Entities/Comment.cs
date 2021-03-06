using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Data.Entities
{
    public class Comment
    {
        public int Id { set; get; }
        public Guid UserId { set; get; }
        public int IdTranslation { set; get; }
        public string Message { set; get; }
        public DateTime DateComment { get; set; }
        public AppUser AppUser { get; set; }
        public Translation Translation { get; set; }
    }
}
