using System.Text.RegularExpressions;

namespace LangApp.BLL.Validation;

public class TextValidation
{
    private static readonly Regex WordRegex = new(
        @"^\p{L}+([-'’]\p{L}+)*$",
        RegexOptions.Compiled
    );
    public static bool IsValidText(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        return WordRegex.IsMatch(input);
    }
}
