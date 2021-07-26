using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Reports
{
    public class ReportViewModel
    {
        public int Id { set; get; }
        public Guid UserId { set; get; }
        public int IdParagraph { set; get; }
        public string Reason { set; get; }
        public string TitleBook { set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Order { get; set; }
    }
}