using LangApp.BLL.LangCode.DTOs;
using LangApp.BLL.Validation;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.LangCode.Commands
{
    public record UpdateLanguageCommand(UpdateLanguageDTO UpdateLanguageDTO) : IRequest<UpdateLanguageDTO>;
    public class UpdateLanguageCommandHandler(ILangCodeRepository repository)
        : IRequestHandler<UpdateLanguageCommand, UpdateLanguageDTO>
    {
        public async Task<UpdateLanguageDTO> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            if(!TextValidation.IsValidText(request.UpdateLanguageDTO.EditedName) 
                && !TextValidation.IsValidText(request.UpdateLanguageDTO.EditedLangCode))
            {
                throw new ArgumentException("Invalid language name or code");
            }
            
            var normalizedEditedName = TextNormalizer.ToNormalized(request.UpdateLanguageDTO.EditedName);
            var normalizedEditedLangCode = TextNormalizer.ToNormalized(request.UpdateLanguageDTO.EditedLangCode);

            var normalizedLangCodeToEdit = TextNormalizer.ToNormalized(request.UpdateLanguageDTO.LangCodeToEdit);

            var existingCategory = await repository.GetLangCodeByCodeAsync(normalizedLangCodeToEdit);

            if(existingCategory != null)
            {
                existingCategory.LangCode = normalizedEditedLangCode;
                existingCategory.Name = normalizedEditedName;

                var result = await repository.UpdateCategoryAsync(existingCategory);

                var response = new UpdateLanguageDTO
                {
                    EditedLangCode = TextNormalizer.ToDisplay(result.LangCode),
                    EditedName = TextNormalizer.ToDisplay(result.Name)
                };

                return response;
            }
            else
            {
                throw new InvalidOperationException("Language to update was not found.");
            }
        }
    }
}
