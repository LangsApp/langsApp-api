using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Models
{
    public class BaseWord
    {
        public Guid Id { get; set; }
        public string NormalizedWord { get; set; } = string.Empty;
        public string DisplayWord { get; set; } = string.Empty;
        public bool Reviewded { get; set; } = false;
        public bool Published { get; set; } = false;

        public ICollection<Translate> Translates { get; set; } = [];
        public ICollection<Category> Categories { get; set; } = [];
    }
}
