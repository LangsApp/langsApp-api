using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using LangApp.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.DAL.Repositories;

public class TranslateRepository(LangAppDBContext dBContext) : ITranslateRepository
{
    public async Task<ICollection<Translate>> GetAllTranslatesAsync()
    {
        return await dBContext.Translate
            .Include(t => t.Word)
            .Include(t => t.Language)
            .ToListAsync(); 
    }

    public async Task<List<Translate>> AddListTranslatesAsync(List<Translate> newTranslates)
    {
        dBContext.Translate.AddRange(newTranslates);
        await dBContext.SaveChangesAsync();
        return newTranslates;
    }

    public async Task<List<Guid>> GetCorrectIDsAnswersAsync(List<Guid> wordIds)
    {
        return await dBContext.Translate
            .Where(t => wordIds.Contains(t.WordId))
            .Select(t => t.WordId)
            .ToListAsync();
    }

    public async Task<List<Translate>> GetAnswersByQuestionsAsync(List<string> questions, Languages nativeLang)
    {
        var normalizedQuestions = questions.Select(q => q.Trim().ToLower()).ToList();
        
        var wordIds = await dBContext.Translate
            .Where(t => normalizedQuestions.Contains(t.NormalizedTranslatedText))
            .Select(t => t.WordId)
            .ToListAsync();

        var correctAnswers = await dBContext.Translate
            .Where(t => wordIds.Contains(t.WordId) && t.LanguageId == nativeLang.Id)
            .ToListAsync();
        
        return correctAnswers;
    }
}
