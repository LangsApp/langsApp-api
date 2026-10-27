using LangApp.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.StageManagment.Command
{
    public record DeleteStageCommand(Guid id) : IRequest<string>;
    public class DeleteStageCommandHandler(IStageRepository repository)
        : IRequestHandler<DeleteStageCommand, string>
    {
        public async Task<string> Handle(DeleteStageCommand command, CancellationToken cancellationToken)
        {
            var existingStage = await repository.GetStageByIdAsync(command.id);

            if(existingStage != null)
            {
                if(!existingStage.Published)
                {
                    await repository.DeleteStageAsync(existingStage);
                    return $"Stage {command.id} deleted";
                }
                else
                {
                    throw new InvalidOperationException("Published stage cannot be deleted");
                }
            }
            else
            {
                throw new Exception(
                    $"Stage with ID: {command.id} was not found");
            }
        }
    }
}
