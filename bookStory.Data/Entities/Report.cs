using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Data.Entities
{
    public class Report
    {
        public int Id { set; get; }
        public Guid UserId { set; get; }
        public int IdParagraph { set; get; }
        public string Reason { set; get; }
        public AppUser AppUser { get; set; }
        public Paragraph Paragraph { get; set; }
    }
}
