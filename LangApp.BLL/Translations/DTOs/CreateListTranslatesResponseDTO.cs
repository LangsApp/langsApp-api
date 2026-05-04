using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Translations.DTOs;

public class CreateListTranslatesResponseDTO
{
    public int Count { get; set; }
    public string Message { get; set; } = string.Empty;
}
