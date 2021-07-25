using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Data.Entities
{
    public class Book
    {
        public int Id { set; get; }
        public string FileName { set; get; }
        public string Title { set; get; }
        public string Author { set; get; } //Tác giả
        public float Rating { set; get; }
        public bool? IsFeatured { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
        public List<Project> Projects { get; set; }
        public List<BookImage> BookImages { get; set; }
    }
}