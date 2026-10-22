using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Reviewded { get; set; }
    public bool Published { get; set; }
    public IEnumerable<BaseWord>? BaseWords { get; set; } = new List<BaseWord>();
}
