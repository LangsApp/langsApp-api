using LangApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Words.DTOs
{
    public class GetBaseWordsDTO
    {
        public List<BaseWord> BaseWords { get; set; } = new List<BaseWord>();
    }
}
