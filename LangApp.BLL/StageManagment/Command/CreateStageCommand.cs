using LangApp.BLL.Validation;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.StageManagment.Command
{
    public record CreateStageCommand(string NewStage, int Order) : IRequest<Stage>;

    public class CreateStageCommandHandler(IStageRepository repository) : IRequestHandler<CreateStageCommand, Stage>
    {
        public async Task<Stage> Handle(CreateStageCommand request, CancellationToken cancellationToken)
        {
            if (!TextValidation.IsValidText(request.NewStage))
            {
                throw new ArgumentException("Invalid stage name.");
            }

            var existingStage = await repository.GetStageByNameAsync(request.NewStage);

            if (existingStage != null)
            {
                throw new InvalidOperationException("A stage with the same name already exists.");
            }

            var entity = new Stage
            {
                StageName = request.NewStage,
                Order = request.Order 
            };

            return await repository.CreateStageAsync(entity);
        }
    }
}
