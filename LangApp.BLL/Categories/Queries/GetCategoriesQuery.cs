using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Categories.Queries
{
    public record GetCategoriesQuery() : IRequest<ICollection<Category>>;
    public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository) 
        : IRequestHandler<GetCategoriesQuery, ICollection<Category>>
    {
        public async Task<ICollection<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await categoryRepository.GetAllCategoriesAsync();
        }
    }
}
