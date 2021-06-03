using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Comments
{
    public class CommentUpdateRequest
    {
        public int Id { set; get; }
        public Guid UserId { set; get; }
        public int IdTranslation { set; get; }
        public string Message { set; get; }
        public DateTime DateComment { get; set; }
    }
}