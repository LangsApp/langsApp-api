using LangApp.BLL.StageManagment.DTOs;
using LangApp.BLL.Validation;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.StageManagment.Command
{
    public record UpdateStageCommand(UpdateStageDTO UpdateStage) : IRequest<UpdateStageDTO>;
    public class UpdateStageCommandHandler(IStageRepository repository) 
        : IRequestHandler<UpdateStageCommand, UpdateStageDTO>
    {
        public async Task<UpdateStageDTO> Handle(UpdateStageCommand request, CancellationToken cancellationToken)
        {
            if(!TextValidation.IsValidText(request.UpdateStage.EditedName))
            {
                throw new ArgumentException("Invalid category name"); 
            }

            var existingStage = await repository.GetStageByNameAsync(request.UpdateStage.NameToEdit);

            if(existingStage != null)
            {
                var normalizedRequest = TextNormalizer.ToNormalized(request.UpdateStage.EditedName);

                existingStage.StageName = normalizedRequest;
                existingStage.Order = request.UpdateStage.EditedOrder;

                var result = await repository.UpdateStageAsync(existingStage);

                var response = new UpdateStageDTO
                {
                    EditedName = TextNormalizer.ToDisplay(result.StageName),
                    EditedOrder = result.Order,
                    NameToEdit = TextNormalizer.ToDisplay(request.UpdateStage.NameToEdit)
                };

                return response;
            }
            else
            {
                throw new InvalidOperationException("Stage to update was not found.");
            }

            
        }
    }
}
