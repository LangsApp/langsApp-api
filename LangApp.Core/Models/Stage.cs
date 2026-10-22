namespace LangApp.Core.Models;

public class Stage
{
    public Guid Id { get; set; }
    public string StageName { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool Reviewded { get; set; }
    public bool Published { get; set; }
}
