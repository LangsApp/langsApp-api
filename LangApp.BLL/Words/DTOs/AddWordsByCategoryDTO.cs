using LangApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Words.DTOs
{
    public class AddWordsByCategoryDTO
    {
        public string CategoryName { get; set; } = string.Empty;
        public List<CreateBaseWordDTO> Words { get; set; } = new List<CreateBaseWordDTO>();
    }
}
