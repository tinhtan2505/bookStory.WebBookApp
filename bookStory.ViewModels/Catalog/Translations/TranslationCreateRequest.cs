using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Translations
{
    public class TranslationCreateRequest
    {
        public Guid UserId { set; get; }
        public int IdParagraph { set; get; }
        public string Text { set; get; }
        public int Rating { set; get; }
    }
}