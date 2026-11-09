using LangApp.BLL.DTOs;
using LangApp.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Translations.Commands
{
    public record ReviewOrPublishTranslateCommand(List<ReviewAndPublishDTO> ReviewAndPublishes)
        : IRequest<ICollection<ReviewAndPublishDTO>>;
    public class ReviewOrPublishTranslateCommandHandler(ITranslateRepository repository)
        : IRequestHandler<ReviewOrPublishTranslateCommand, ICollection<ReviewAndPublishDTO>>
    {
        public async Task<ICollection<ReviewAndPublishDTO>> Handle(ReviewOrPublishTranslateCommand request,
            CancellationToken cancellationToken)
        {
            var unpostedItems = new List<ReviewAndPublishDTO>();
            foreach(var item in request.ReviewAndPublishes)
            {
                if (!item.Reviewed && item.Published)
                {
                    unpostedItems.Add(item);
                }
            }

            if (unpostedItems.Count > 0)
            {
                throw new InvalidOperationException("Cannot publish items that have not been reviewed.");
            }

            var existingItems = await repository.GetTranslatesByIdsAsync(request.ReviewAndPublishes.Select(t => t.Id).ToList());

            foreach (var item in existingItems)
            {
                var dto = request.ReviewAndPublishes.FirstOrDefault(t => t.Id == item.Id);

                if(dto != null)
                {
                    item.Published = dto.Published;
                    item.Reviewed = dto.Reviewed;
                }
            }

            var result = await repository.ReviewedOrPublishedTranslatesAsync(existingItems);

            if(result != null)
            {
                var response = result.Select(x => new ReviewAndPublishDTO
                {
                    Id = x.Id,
                    Published = x.Published,
                    Reviewed = x.Reviewed
                }).ToList();

                return response;
            }
            else
            {
                throw new InvalidOperationException("Failed to update the translates.");
            }
        }
    }
}
