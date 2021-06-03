using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Comments
{
    public class CommentCreateRequest
    {
        public Guid UserId { set; get; }
        public int IdTranslation { set; get; }
        public string Message { set; get; }
    }
}