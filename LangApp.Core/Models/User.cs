using Microsoft.AspNetCore.Identity;

namespace LangApp.Core.Models;

public class User : IdentityUser 
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;   
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public IEnumerable<Progress> Progresses { get; set; } = new List<Progress>();
}
