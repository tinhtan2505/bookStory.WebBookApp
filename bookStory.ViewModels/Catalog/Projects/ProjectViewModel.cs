using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Projects
{
    public class ProjectViewModel
    {
        public int Id { set; get; }
        public int IdBook { set; get; }

        public string IdLanguage { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public Guid UserId { set; get; } //người yêu cầu
        public int Status { set; get; }
        public DateTime DateProject { get; set; }
        public string TitleBook { set; get; }
        public string NameLanguage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}