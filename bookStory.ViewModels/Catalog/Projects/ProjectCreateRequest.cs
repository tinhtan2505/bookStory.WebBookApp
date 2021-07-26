using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Projects
{
    public class ProjectCreateRequest
    {
        public int IdBook { set; get; }
        public string IdLanguage { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public Guid UserId { set; get; }
    }
}