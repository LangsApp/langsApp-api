using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Models;

public class Progress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int WordId { get; set; }
    public int StageId { get; set; }

    public User? User { get; set; }
    public BaseWord? Word { get; set; }
    public Stage? Stage { get; set; }    
}
