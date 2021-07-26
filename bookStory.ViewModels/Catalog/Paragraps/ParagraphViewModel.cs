using bookStory.ViewModels.Catalog.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Paragraps
{
    public class ParagraphViewModel
    {
        public int Id { set; get; }
        public int IdBook { set; get; }
        public string Order { set; get; }
        public string Type { set; get; }
        public string RatingMax { get; set; }
        public string TitleBook { set; get; }
    }
}