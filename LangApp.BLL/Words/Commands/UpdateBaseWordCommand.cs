using LangApp.BLL.Translations.Services;
using LangApp.BLL.Validation;
using LangApp.BLL.Words.DTOs;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.BLL.Words.Commands
{
    public record UpdateBaseWordCommand(UpdateBaseWordDTO UpdateBaseWordDTO) : IRequest<UpdateBaseWordDTO>;
    public class UpdateBaseWordCommandHandler(IBaseWordRepository repository, 
        UpdateTranslatesByUpdatedBaseWord updateTranslates)
        : IRequestHandler<UpdateBaseWordCommand, UpdateBaseWordDTO>
    {
        public  async Task<UpdateBaseWordDTO> Handle(UpdateBaseWordCommand request, CancellationToken cancellationToken)
        {
            if(!TextValidation.IsValidText(request.UpdateBaseWordDTO.EditedWord))
            {
                throw new ArgumentException("Invalid word");
            }

            var normalizedEditedWord = TextNormalizer.ToNormalized(request.UpdateBaseWordDTO.EditedWord);
            var displayedEditedWord = TextNormalizer.ToDisplay(request.UpdateBaseWordDTO.EditedWord);

            //var normalizedWordToEdit = TextNormalizer.ToNormalized(request.UpdateBaseWordDTO.WordToEdit);

            var existingBaseWord = await repository.GetBaseWordByIdAsync(request.UpdateBaseWordDTO.Id);

            if(existingBaseWord != null)
            {
                existingBaseWord.NormalizedWord = normalizedEditedWord;
                existingBaseWord.DisplayWord = displayedEditedWord;

                var wordResult = await repository.UpdateBaseWordAsync(existingBaseWord);

                await updateTranslates.UpdateForBaseWordAsync(wordResult.Id, wordResult.NormalizedWord, cancellationToken);

                var response = new UpdateBaseWordDTO
                {
                    Id = wordResult.Id,
                    EditedWord = wordResult.DisplayWord
                };

                return response;
            }
            else
            {
                throw new InvalidOperationException("Base word to update was not found.");
            }
        }
    }
}
