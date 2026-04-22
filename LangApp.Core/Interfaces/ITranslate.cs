using LangApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Interfaces
{
    public interface ITranslate
    {
        Task<ICollection<Translate>> GetAllTranslatesAsync();
        Task<List<Translate>> AddListTranslatesAsync(List<Translate> newTranslates);
    }
}
