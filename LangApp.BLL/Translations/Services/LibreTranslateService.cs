using LangApp.BLL.Translations.DTOs;
using LangApp.BLL.Validation;
using LangApp.Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Translations.Services
{
    public class LibreTranslateService(HttpClient httpClient) : ILibreTranslateService
    {
        // Додати перевірку на те що приходить із LibreTranslate
        // Розібратися із "?" в сигнатурі методу
        // Вирішити питання із кирилицею
        public async Task<string?> TranslateAsync(string text, string sourceLang, 
            string targetLang, CancellationToken cancellationToken)
        {
            var response = await httpClient.PostAsJsonAsync("http://localhost:5000/trabslate",
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
