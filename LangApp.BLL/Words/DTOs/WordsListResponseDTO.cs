using LangApp.Core.Models;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Words.DTOs
{
    public class WordsListResponseDTO
    {
        public List<string> Words { get; set; } = new List<string>();
        public string CategoryName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
