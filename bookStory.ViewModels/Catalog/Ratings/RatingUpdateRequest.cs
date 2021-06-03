using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Ratings
{
    public class RatingUpdateRequest
    {
        public int Id { set; get; }
        public Guid UserId { set; get; }
        public int IdTranslation { set; get; }
        public int Vote { set; get; }
    }
}