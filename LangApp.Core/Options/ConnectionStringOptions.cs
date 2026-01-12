namespace LangApp.Core.Options;

public record ConnectionStringOptions
{
    public const string SectionName = "ConnectionStrings";
    public string? DefaultConnection { get; set; }
}
