using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Data.Entities
{
    public class Paragraph
    {
        public int Id { set; get; }
        public int IdBook { set; get; }
        public string Order { set; get; }
        public string Type { set; get; }
        public List<Report> Reports { get; set; }
        public List<Translation> Translations { get; set; }
        public Book Book { get; set; }
    }
}
