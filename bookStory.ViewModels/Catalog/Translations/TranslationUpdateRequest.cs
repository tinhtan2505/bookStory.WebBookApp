using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Translations
{
    public class TranslationUpdateRequest
    {
        public int Id { set; get; }
        public Guid UserId { set; get; }
        public int IdProject { set; get; }
        public int IdParagraph { set; get; }
        public string Text { set; get; }
        public string Rating { set; get; }
        public DateTime Date { get; set; }
    }
}