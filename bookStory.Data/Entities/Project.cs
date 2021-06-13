using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Data.Entities
{
    public class Project
    {
        public int Id { set; get; }
        public int IdBook { set; get; }
        public string IdLanguage { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public Guid UserId { set; get; } //người yêu cầu
        public int Status { set; get; }
        public DateTime DateProject { get; set; }
        public Book Book { get; set; }
        public Language Language { get; set; }
        public AppUser AppUser { get; set; }
        //public object DateTime { get; set; }
    }
}