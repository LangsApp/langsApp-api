using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Interfaces
{
    public interface ILibreTranslateService
    {
        Task<string?>  TranslateAsync(string text, string sourceLang, 
            string targetLang, CancellationToken cancellationToken);
    }
}
