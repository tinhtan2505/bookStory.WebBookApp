using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Data.Entities
{
    public class Translation
    {
        public int Id { set; get; }
        public Guid UserId { set; get; }

        //public int IdProject { set; get; }
        public int IdParagraph { set; get; }

        public string Text { set; get; }
        public int Rating { set; get; }
        public DateTime Date { get; set; }
        public AppUser AppUser { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Rating> Ratings { get; set; }
        public Paragraph Paragraph { get; set; }
        //public Project Project { get; set; }
    }
}