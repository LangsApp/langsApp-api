using LangApp.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.LangCode.Commands
{
    public record DeleteLanguageCommand(Guid id) : IRequest<string>;
    public class DeleteLanguageCommandHandler(ILangCodeRepository repository)
        :IRequestHandler<DeleteLanguageCommand, string>
    {
        public async Task<string> Handle(DeleteLanguageCommand command, CancellationToken cancellationToken)
        {
            var existingLanguage = await repository.GetLangCodeByIdAsync(command.id);

            if(existingLanguage != null)
            {
                if(!existingLanguage.Published)
                {
                    await repository.DeleteLanguageAsync(existingLanguage);
                    return $"Language {command.id} deleted";
                }
                else
                {
                    throw new InvalidOperationException("Published language cannot be deleted");
                }
            }
            else
            {
                throw new Exception(
                    $"Language with ID: {command.id} was not found");
            }
        }
    }
}
