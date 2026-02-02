namespace LangApp.Core.Models;

public class Stage
{
    public Guid Id { get; set; }
    public string StageName { get; set; } = string.Empty;
    public int Order { get; set; }
}
