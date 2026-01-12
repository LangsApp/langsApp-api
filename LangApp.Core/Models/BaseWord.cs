using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Models
{
    public class BaseWord
    {
        public int Id { get; set; }
        public string NormalizedWord { get; set; } = string.Empty;
        public string? DisplayWord { get; set; } 

        public IEnumerable<Translate>? Translates { get; set; } = [];
        public IEnumerable<Category>? Categories { get; set; } = [];
    }
}
