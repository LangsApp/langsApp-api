using LangApp.Core.Interfaces.Repository;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Categories.Commands
{
    public record DeleteCategoryCommand(Guid id) : IRequest<string>;
    public class DeleteCategoryCommandHandler(ICategoryRepository repository)
        : IRequestHandler<DeleteCategoryCommand, string>
    {
        public async Task<string> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var existingCategory = await repository.GetCategoryByIdAsync(command.id);

            if(existingCategory != null)
            {
                if (!existingCategory.Published)
                {
                    await repository.DeleteCategoryAsync(existingCategory);
                    return $"Category {command.id} deleted";
                }
                else
                {
                    throw new InvalidOperationException("Published category cannot be deleted.");
                }
            }
            else
            {
                throw new Exception(
                    $"Category with ID: {command.id} was not found");
            }
        }
    }
}
