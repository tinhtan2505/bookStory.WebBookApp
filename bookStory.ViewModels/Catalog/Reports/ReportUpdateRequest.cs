using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Reports
{
    public class ReportUpdateRequest
    {
        public int Id { set; get; }
        public Guid UserId { set; get; }
        public int IdParagraph { set; get; }
        public string Reason { set; get; }
    }
}