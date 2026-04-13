using LangApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Words.Services;

public class WordService
{
    public static BaseWord NormalizedWord(BaseWord word)
    {
        if (string.IsNullOrWhiteSpace(word.NormalizedWord))
            throw new ArgumentException("Word cannot be empty");

        var lower = word.NormalizedWord.ToLower();

        word.NormalizedWord = lower;
        word.DisplayWord = char.ToUpper(lower[0]) + lower.Substring(1);

        return word;
    }
}
