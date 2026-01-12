using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Core.Models;

public class Languages
{
    public int Id { get; set; }
    public string LangCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public IEnumerable<Translate>? Translates { get; set; } = new List<Translate>();
}
