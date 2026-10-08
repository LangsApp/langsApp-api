using LangApp.BLL.Translations.DTOs;
using LangApp.BLL.Validation;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Translations.Commands
{
    public record UpdateTranslateCommand(UpdateTranslateDTO UpdatedTranslate) : IRequest<UpdateTranslateDTO>;
    public class UpdateTranslateCommandHandler(ITranslateRepository repository)
        : IRequestHandler<UpdateTranslateCommand, UpdateTranslateDTO>
    {
        public async Task<UpdateTranslateDTO> Handle(UpdateTranslateCommand request, CancellationToken cancellationToken)
        {
            if(!TextValidation.IsValidText(request.UpdatedTranslate.EditedTranslate))
            {
                throw new ArgumentException("Invalid translate name");
            }

            var normalizedNameToEdit = TextNormalizer.ToNormalized(request.UpdatedTranslate.TranslateToEdit);

            var existingTranslate = await repository.GetTranslateByName(normalizedNameToEdit);

            if (existingTranslate != null)
            {

                existingTranslate.NormalizedTranslatedText = TextNormalizer.ToNormalized(request.UpdatedTranslate.EditedTranslate);
                existingTranslate.DisplayTranslatedText = TextNormalizer.ToDisplay(request.UpdatedTranslate.EditedTranslate);

                var result = await repository.UpdateTranslateAsync(existingTranslate);

                var response = new UpdateTranslateDTO
                {
                    TranslateToEdit = request.UpdatedTranslate.TranslateToEdit,
                    EditedTranslate = result.DisplayTranslatedText
                };

                return response;
            }
            else
            {
                throw new InvalidOperationException("Translate to update was not found.");
            }
        }
    }
}
