using LangApp.Core.Models;

namespace LangApp.Core.Interfaces;

public interface IBaseWord
{
    Task<BaseWord> CreateBaseWordAsync(BaseWord newWord);
}
