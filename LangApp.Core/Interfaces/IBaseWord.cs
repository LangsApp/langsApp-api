using LangApp.Core.Models;

namespace LangApp.Core.Interfaces;

public interface IBaseWord
{
    Task<BaseWord> CreateBaseWordAsync(BaseWord newWord);
    Task <List<BaseWord>> AddListWordsAsync(List<BaseWord> newWords);
    Task<BaseWord?> GetBaseWordByNameAsync(string normalizedWord);
    Task<ICollection<BaseWord>> GetAllBaseWordsAsync();
    Task<HashSet<string>> GetAllNormalizedWordsAsync();
}
