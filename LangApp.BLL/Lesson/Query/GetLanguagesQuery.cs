using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Lesson.Query
{
    public record GetLanguagesQuery() : IRequest<ICollection<Languages>>;
    

    public class GetLanguagesQueryHandler(ILangCodeRepository langCodeRepo) 
        : IRequestHandler<GetLanguagesQuery, ICollection<Languages>>
    {
        public async Task<ICollection<Languages>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            return await langCodeRepo.GetAllLanguagesAsync();
        }
    }
}
