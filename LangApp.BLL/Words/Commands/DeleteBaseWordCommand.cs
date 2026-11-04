using LangApp.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Words.Commands
{
    public record DeleteBaseWordCommand(Guid id) : IRequest<string>;
    public class DeleteBaseWordCommandHandler(IBaseWordRepository repository)
        : IRequestHandler<DeleteBaseWordCommand, string>
    {
        public async Task<string> Handle(DeleteBaseWordCommand command, CancellationToken cancellationToken)
        {
            var existingBaseWord = await repository.GetBaseWordByIdAsync(command.id);

            if(existingBaseWord != null)
            {
                if (!existingBaseWord.Published)
                {
                    await repository.DeleteBaseWordAsync(existingBaseWord);
                    return $"BaseWord {command.id} deleted";
                }
                else
                {
                    throw new InvalidOperationException("Published base word cannot be deleted.");
                }
            }
            else
            {
                throw new Exception(
                    $"Base word with ID: {command.id} was not found");
            }
        }
    }
}
