using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStory.WebBookApp.Models
{
    public class ProjectRequestViewModel
    {
        public int IdBook { set; get; }

        public string IdLanguage { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public string UserName { get; set; }
    }
}