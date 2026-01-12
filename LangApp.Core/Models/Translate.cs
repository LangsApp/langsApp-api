using LangApp.Core.Models;


namespace LangApp.Core.Models;

public class Translate
{
    public int Id { get; set; }
    public int WordId { get; set; }
    public int LangCodeId { get; set; }
    public string NormalizedTranslatedText { get; set; } = string.Empty;
    public string? DisplayTranslatedText { get; set; }
    public BaseWord? Word { get; set; } 
    public Languages? Language { get; set; }
}
