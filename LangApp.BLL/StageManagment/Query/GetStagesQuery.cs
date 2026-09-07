using LangApp.BLL.StageManagment.DTOs;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.StageManagment.Query
{
    public record GetStagesQuery() : IRequest<ICollection<GetStagesDTO>>;
    public class GetStagesQueryHandler(IStageRepository stageRepository)
        : IRequestHandler<GetStagesQuery, ICollection<GetStagesDTO>>
    {
        public async Task<ICollection<GetStagesDTO>> Handle(GetStagesQuery request, CancellationToken cancellationToken)
        {
            var entity = await stageRepository.GetAllStagesAsync();

            var response = entity.Select(s => new GetStagesDTO
            {
                Name = s.StageName,
                Order = s.Order
            }).ToList();

            return response;
        }

    }
}
