using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Interfaces.Services
{
    public static class TextNormalizer
    {
        public static string ToNormalized(string text)
        {
            
            return text.ToLower();
        }

        public static string ToDisplay(string text)
        {
            var lower = text.ToLower();

            return char.ToUpper(lower[0]) + lower.Substring(1);
        }
    }
}
