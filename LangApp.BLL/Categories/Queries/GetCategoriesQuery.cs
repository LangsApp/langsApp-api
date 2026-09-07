using LangApp.BLL.Categories.DTOs;
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
    public record GetCategoriesQuery() : IRequest<ICollection<GetCategoryDTO>>;
    public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository) 
        : IRequestHandler<GetCategoriesQuery, ICollection<GetCategoryDTO>>
    {
        public async Task<ICollection<GetCategoryDTO>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await categoryRepository.GetAllCategoriesAsync();

            var response = result.Select(c => new GetCategoryDTO
            {
                Name = c.Name,
            }).ToList();

            return response;
        }
    }
}
