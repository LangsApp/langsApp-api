using LangApp.BLL.Validation;
using LangApp.Core.Interfaces.Services;
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
            language.Name = TextNormalizer.ToDisplay(language.Name);
            language.LangCode = TextNormalizer.ToNormalized(language.LangCode);
            return language;
        }
    }
}
