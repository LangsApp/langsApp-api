using LangApp.Core.Models;


namespace LangApp.Core.Models;

public class Translate
{
    public Guid Id { get; set; }
    public Guid WordId { get; set; }
    public Guid LanguageId { get; set; }
    public string NormalizedTranslatedText { get; set; } = string.Empty;
    public string? DisplayTranslatedText { get; set; }
    public BaseWord? Word { get; set; } 
    public Languages? Language { get; set; }
}
