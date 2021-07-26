using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Paragraps
{
    public class ParagraphDeleteRequest
    {
        public int Id { get; set; }
        public string TitleBook { set; get; }
        public string Order { set; get; }
    }
}