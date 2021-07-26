using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Ratings
{
    public class TimKiemRatingPagingRequest : PagingRequestBase
    {
        public Guid keywordUserId { get; set; }
        public int keywordIdTranslation { get; set; }
    }
}