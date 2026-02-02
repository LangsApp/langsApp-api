using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Words.DTOs;

public class CreateBaseWordDTO
{
    public string NormalizedWord { get; set; } = null!;
    public string DisplayWord { get; set; } = null!;
}
