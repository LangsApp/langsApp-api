using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Exceptions
{
    public class ConflictException(string message) : Exception(message)
    {
    }
}
