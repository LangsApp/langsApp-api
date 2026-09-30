using LangApp.BLL.Categories.DTOs;
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
    public record UpdateCategoryCommand(UpdateCategoryDTO UpdateCategory) : IRequest<UpdateCategoryDTO>;
    public class UpdateCategoryCommandHandler(ICategoryRepository repository)
        : IRequestHandler<UpdateCategoryCommand, UpdateCategoryDTO>
    {
        public async Task<UpdateCategoryDTO> Handle(UpdateCategoryCommand reqest, CancellationToken cancellationToken)
        {
            if(!TextValidation.IsValidText(reqest.UpdateCategory.EditedCategory))
            {
                throw new ArgumentException("Invalid category name");
            }

            var normalizedRequest = TextNormalizer.ToNormalized(reqest.UpdateCategory.EditedCategory);

            var existingCategory = await repository.GetCategoryByNameAsync(reqest.UpdateCategory.CategoryToEditName);

            if(existingCategory != null)
            {
                existingCategory.Name = normalizedRequest;
                var result = await repository.UpdateCategoryAsync(existingCategory);

                var response = new UpdateCategoryDTO
                {
                    EditedCategory = TextNormalizer.ToDisplay(result.Name),
                };

                return response;
            }
            else
            {
                throw new InvalidOperationException("Category to update was not found.");
            }
        }
    }
}
