using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Reports
{
    public class ReportDeleteRequest
    {
        public int Id { get; set; }
        public string Reason { set; get; }
        public string TitleBook { set; get; }
        public string Order { get; set; }
    }
}