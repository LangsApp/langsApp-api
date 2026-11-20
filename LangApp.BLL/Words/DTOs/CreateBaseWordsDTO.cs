using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Words.DTOs;

public class CreateBaseWordsDTO
{
    public List<string> BaseWord { get; set; } = new List<string>();
    public string Message { get; set; } = string.Empty;
}
