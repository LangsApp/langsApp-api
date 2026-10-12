using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Words.DTOs
{
    public class UpdateBaseWordDTO
    {
        public string WordToEdit { get; set; } = string.Empty;
        public string EditedWord { get; set; } = string.Empty;
    }
}
