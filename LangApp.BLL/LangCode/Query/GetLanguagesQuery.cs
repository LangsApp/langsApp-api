using LangApp.BLL.LangCode.DTOs;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.LangCode.Query
{
    public record GetLanguagesQuery() : IRequest<ICollection<LangCodeDTO>>;
    

    public class GetLanguagesQueryHandler(ILangCodeRepository langCodeRepo) 
        : IRequestHandler<GetLanguagesQuery, ICollection<LangCodeDTO>>
    {
        public async Task<ICollection<LangCodeDTO>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            var entity = await langCodeRepo.GetAllLanguagesAsync();

            var response = entity.Select(l => new LangCodeDTO
            {
                Code = l.LangCode,
                Name = l.Name,
            }).ToList();

            return response;
        }
    }
}
