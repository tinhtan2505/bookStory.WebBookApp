using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Paragraps
{
    public class ParagraphUpdateRequest
    {
        public int Id { set; get; }
        public int IdBook { set; get; }
        public string Order { set; get; }
        public string Type { set; get; }
    }
}