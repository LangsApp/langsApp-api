using LangApp.Core.Interfaces;
using LangApp.Core.Models;
using LangApp.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LangApp.DAL.Repositories
{
    public class LangCodeRepository(LangAppDBContext dbContext, ILogger<LangCodeRepository> _logger) : ILangCode
    {
        public async Task<Languages> CreateLanguageAsync(Languages newLanguage)
        {
            _logger.LogInformation("Creating new language: {LanguageName} ({LanguageCode})",
                    newLanguage.Name, newLanguage.LangCode);

            newLanguage.Id = Guid.NewGuid();
            dbContext.Languages.Add(newLanguage);
            await dbContext.SaveChangesAsync();
            return newLanguage;
        }

        public async Task<ICollection<Languages>> GetAllLanguagesAsync()
        {
            _logger.LogInformation("Retrieving all languages from the database.");

            return await dbContext.Languages.ToListAsync();
        }

        public Task<Languages?> GetLangCodeByCodeAsync(string langCode)
        {
            _logger.LogInformation("Retrieving language by code: {LanguageCode}", langCode);

            return dbContext.Languages.FirstOrDefaultAsync(l => l.LangCode == langCode);
        }

        public Task<Languages?> GetLangCodeByNameAsync(string langName)
        {
            _logger.LogInformation("Retrieving language by name: {LanguageName}", langName);

            return dbContext.Languages.FirstOrDefaultAsync(l => l.Name == langName);
        }
    }
}
