using LangApp.Core.Models;


namespace LangApp.Core.Models;

public class Translate
{
    public Guid Id { get; set; }
    public Guid WordId { get; set; }
    public Guid LanguageId { get; set; }
    public string NormalizedTranslatedText { get; set; } = string.Empty;
    public string DisplayTranslatedText { get; set; } = string.Empty;
    public bool Reviewded { get; set; } = false;
    public bool Published { get; set; } = false;
    public BaseWord? Word { get; set; } 
    public Languages? Language { get; set; }
}
