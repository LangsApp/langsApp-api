using LangApp.BLL.Validation;
using LangApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.LangCode.Services
{
    public class LangCodeService
    {
        public static Languages NormalizeLanguage(Languages language)
        {
            language.Name = char.ToUpper(language.Name[0]) + language.Name.Substring(1).ToLower();
            language.LangCode = language.LangCode.ToLower();
            return language;
        }
    }
}
