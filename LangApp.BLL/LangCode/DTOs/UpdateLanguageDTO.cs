using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.LangCode.DTOs
{
    public class UpdateLanguageDTO
    {
        public string LangCodeToEdit { get; set; } = string.Empty;

        public string EditedName { get; set; } = string.Empty;
        public string EditedLangCode { get; set; } = string.Empty;
    }
}
