using LangApp.BLL.Categories.DTOs;
using LangApp.BLL.Exceptions;
using LangApp.BLL.Validation;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Interfaces.Services;
using LangApp.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Categories.Commands
{
    public record CreateCategoryCommand(CreateCategoryDTO NewCategory) : IRequest<CreateCategoryDTO>;
    public class CreateCategoryCommandHandler(ICategoryRepository repository) 
        : IRequestHandler<CreateCategoryCommand, CreateCategoryDTO>
    {
        public async Task<CreateCategoryDTO> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
           if(!TextValidation.IsValidText(request.NewCategory.Name))
           {
                throw new ArgumentException("Invalid category name.");
           }

           var normailzedRequest = TextNormalizer.ToNormalized(request.NewCategory.Name);

            var entity = new Category
            {
                Name = normailzedRequest
            };

           

            var existingCategory = await repository.GetCategoryByNameAsync(entity.Name);

            if (existingCategory != null)
            {
                throw new ConflictException("This language already exists.");
            }

            var result = await repository.AddCategoryAsync(entity);

            var response = new CreateCategoryDTO
            {
                Name = TextNormalizer.ToDisplay(result.Name)
            };

            return response;
        }
    }
}
