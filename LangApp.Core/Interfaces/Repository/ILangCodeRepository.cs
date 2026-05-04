using LangApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Interfaces.Repository
{
    public interface ILangCodeRepository
    {
        Task<Languages> CreateLanguageAsync(Languages language);
        Task<ICollection<Languages>> GetAllLanguagesAsync();
        Task<Languages?> GetLangCodeByNameAsync(string langName);
        Task<Languages?> GetLangCodeByCodeAsync(string langCode);
    }
}
