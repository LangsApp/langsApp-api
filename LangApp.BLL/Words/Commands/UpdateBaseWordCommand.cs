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
    public class UpdateBaseWordCommandHandler(IBaseWordRepository repository)
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

                var result = await repository.UpdateBaseWordAsync(existingBaseWord);

                var response = new UpdateBaseWordDTO
                {
                    Id = result.Id,
                    EditedWord = result.DisplayWord
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
