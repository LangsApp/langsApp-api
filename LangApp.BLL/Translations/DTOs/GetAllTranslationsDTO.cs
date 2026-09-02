using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Translations.DTOs
{
    public class GetAllTranslationsDTO
    {
        public string BaseWord { get; set; } = string.Empty;
        public string TranslateTo { get; set; } = string.Empty;
        public string NormalizedTranslatedWord { get; set; } = string.Empty;
        public string DisplayTranslatedWord { get; set ; } = string.Empty;
    }
}
