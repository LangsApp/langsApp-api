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
    public record GetStagesQuery() : IRequest<ICollection<Stage>>;
    public class GetStagesQueryHandler(IStageRepository stageRepository)
        : IRequestHandler<GetStagesQuery, ICollection<Stage>>
    {
        public async Task<ICollection<Stage>> Handle(GetStagesQuery request, CancellationToken cancellationToken)
        {
            return await stageRepository.GetAllStagesAsync();
        }

    }
}
