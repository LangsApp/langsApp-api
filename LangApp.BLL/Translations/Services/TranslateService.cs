using LangApp.BLL.Translations.DTOs;
using LangApp.BLL.Validation;
using LangApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Translations.Services
{
    public class TranslateService(HttpClient httpClient) : ITranslateService
    {
        public async Task<string?> TranslateAsync(string text, string sourceLang, 
            string targetLang, CancellationToken cancellationToken)
        {
            var response = await httpClient.PostAsJsonAsync("http://localhost:5000/translate",
                new
                {
                    q = text,
                    source = sourceLang,
                    target = targetLang,
                    format = "text"
                },
                cancellationToken);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<LibreTranslateResponseDTO>(cancellationToken);

            return result?.TranslatedText;
        }
    }
}
