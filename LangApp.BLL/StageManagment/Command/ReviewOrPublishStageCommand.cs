using LangApp.BLL.DTOs;
using LangApp.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.StageManagment.Command
{
    public record ReviewOrPublishStageCommand(List<ReviewAndPublishDTO> ReviewAndPublish) 
        : IRequest<ICollection<ReviewAndPublishDTO>>;
    public class ReviewOrPublishStageCommandHandler(IStageRepository repository)
        : IRequestHandler<ReviewOrPublishStageCommand, ICollection<ReviewAndPublishDTO>>
    {
        public async Task<ICollection<ReviewAndPublishDTO>> Handle(ReviewOrPublishStageCommand request, 
            CancellationToken cancellationToken)
        {
            var unpostedItems = new List<ReviewAndPublishDTO>();
            foreach(var item in request.ReviewAndPublish)
            {
                if(!item.Reviewed && item.Published)
                {
                    unpostedItems.Add(item);
                }
            }
            if (unpostedItems.Count > 0)
            { // in future, we can return a list of unposted items to the user with own
              // exception instead of throwing an exception
                throw new InvalidOperationException("Cannot publish items that have not been reviewed.");
            }
            var existingItems = await repository.GetStagesByIdsAsync(request.ReviewAndPublish.Select(x => x.Id).ToList());

            foreach (var item in existingItems)
            {
                var dto = request.ReviewAndPublish.FirstOrDefault(x => x.Id == item.Id);
                if (dto != null)
                {
                    item.Reviewded = dto.Reviewed;
                    item.Published = dto.Published;
                }
            }

            var result = await repository.ReviewedOrPublishedStagesAsync(existingItems);

            if (result != null)
            {
                var response = result.Select(x => new ReviewAndPublishDTO
                {
                    Id = x.Id,
                    Reviewed = x.Reviewded,
                    Published = x.Published
                }).ToList();

                return response;
            }
            else
            {
                throw new InvalidOperationException("Failed to update stages.");
            }
               
        }
    }
}
