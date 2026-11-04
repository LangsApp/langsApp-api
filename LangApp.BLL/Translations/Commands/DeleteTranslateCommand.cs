using LangApp.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Translations.Commands
{
    public record DeleteTranslateCommand(Guid id) : IRequest<string>;
    public class DeleteTranslateCommandHandler(ITranslateRepository repository)
        : IRequestHandler<DeleteTranslateCommand, string>
    {
        public async Task<string> Handle(DeleteTranslateCommand command, CancellationToken cancellationToken)
        {
            var existingTranslate = await repository.GetTranslateByIdAsync(command.id);

            if (existingTranslate != null)
            {
                if (!existingTranslate.Published)
                {
                    await repository.DeleteTranslateAsync(existingTranslate);
                    return $"Translate {command.id} deleted";
                }
                else
                {
                    throw new InvalidOperationException("Published translate cannot be deleted.");
                }
            }
            else
            {
                throw new Exception(
                    $"Translate with ID: {command.id} was not found");
            }
        }
    }

}
