using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Words.DTOs
{
    public class CreateBaseWordByCategoryDTO
    {
        public string BaseWord { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
    }
}
