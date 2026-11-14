using LangApp.BLL.DTOs;
using LangApp.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Words.Commands
{
    public record ReviewOrPublishBaseWordCommand(List<ReviewAndPublishDTO> ReviewAndPublish)
        : IRequest<ICollection<ReviewAndPublishDTO>>;
    public class ReviewOrPublishBaseWordCommandHandler(IBaseWordRepository repository)
        : IRequestHandler<ReviewOrPublishBaseWordCommand, ICollection<ReviewAndPublishDTO>>
    {
        public async Task<ICollection<ReviewAndPublishDTO>> Handle(ReviewOrPublishBaseWordCommand request,
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
            {
                // in future, we can return a list of unposted items to the user with own
                // exception instead of throwing an exception
                throw new InvalidOperationException("Cannot publish items that have not been reviewed.");
            }

            var existingItems = await repository.GetCollectionBaseWordsByIdsAsync(request.ReviewAndPublish
                .Select(bw => bw.Id).ToList());

            foreach (var item in existingItems)
            {
                var dto = request.ReviewAndPublish.FirstOrDefault(bw => bw.Id == item.Id);
                if(dto != null)
                {
                    if (item.Published && !dto.Published)
                    {
                        throw new InvalidOperationException(
                            "Published item cannot be unpublished.");
                    }
                    item.Reviewed = dto.Reviewed;
                    item.Published = dto.Published;
                }
            }

            var result = await repository.ReviewedOrPublishedBaseWordsAsync(existingItems);

            if(result != null)
            {
                var response = result.Select(bw => new ReviewAndPublishDTO
                {
                    Id = bw.Id,
                    Reviewed = bw.Reviewed,
                    Published = bw.Published,
                }).ToList();
                
                return response;
            }
            else
            {
                throw new InvalidOperationException("Failed to update the base words.");
            }
        }
    }
}
