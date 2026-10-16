using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Translations.DTOs
{
    public class UpdateTranslateDTO
    {
        public Guid Id { get; set; }
        public string TranslateToEdit { get; set; } = string.Empty;
        public string EditedTranslate { get; set; } = string.Empty;
    }
}
