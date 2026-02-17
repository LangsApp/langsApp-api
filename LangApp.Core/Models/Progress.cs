using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Models;

public class Progress
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public Guid WordId { get; set; }
    public Guid StageId { get; set; }

    public User? User { get; set; }
    public BaseWord? Word { get; set; }
    public Stage? Stage { get; set; }    
}
