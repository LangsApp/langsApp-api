using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Categories.DTOs
{
    public class UpdateCategoryDTO
    {
        public Guid CategoryToEditId { get; set; }
        public string CategoryToEditName { get; set; } = string.Empty;
        public string EditedCategory { get; set; } = string.Empty;
    }
}
