using AutoMapper;
using LangApp.BLL.Exceptions;
using LangApp.BLL.Validation;
using LangApp.BLL.Words.DTOs;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Interfaces.Services;
using LangApp.Core.Models;
using MediatR;

namespace LangApp.BLL.Words.Commands;

public record CreateBaseWordsCommand(CreateBaseWordsDTO NewWord) : IRequest<CreateBaseWordsDTO>;

public class CreateBaseWordCommandHandler(IBaseWordRepository repository) 
    : IRequestHandler<CreateBaseWordsCommand, CreateBaseWordsDTO>
{
    public async Task<CreateBaseWordsDTO> Handle(CreateBaseWordsCommand request, CancellationToken cancellationToken)
    {   
        var validWords = new List<string>();
        var invalidWords = new List<string>();
        var message = new List<string>();
        
        var inputWords = request.NewWord.BaseWord.Select(w => w.Trim()).Distinct().ToList();
        //var noralizedWord = WordService.NormalizedWord(entity);

        foreach (var word in inputWords)
        {
            if (!TextValidation.IsValidText(word))
            {
                invalidWords.Add(word);
                continue;
            }
            validWords.Add(word);
        }

        if (validWords.Count == 0)
        {
            throw new ArgumentException("No valid words to add.");
        }

        var normalizedWords = new List<BaseWord>(
            validWords.Select(word => new BaseWord
            {
                NormalizedWord = TextNormalizer.ToNormalized(word),
                DisplayWord = TextNormalizer.ToDisplay(word)
            }));

        
            var existWords = await repository.GetExistingNormalizedWordsAsync(normalizedWords);

            var wordsToInsert = normalizedWords.Where(w => !existWords.Contains(w.NormalizedWord)).ToList();

            var skippedWords = normalizedWords.Where(w => existWords.Contains(w.NormalizedWord))
                .Select(w => w.NormalizedWord).ToList();

            if (wordsToInsert.Count == 0)
            {
                throw new ConflictException("Words already exist");
            }

            var result = await repository.AddListWordsAsync(wordsToInsert);
 
            if (skippedWords.Count > 0)
            {
                message.Add($"Some words were skipped because they already exist: {string.Join(", ", skippedWords)}\n");
            }

            if (invalidWords.Count > 0)
            {
                message.Add($"Some words were skipped because they contain invalid characters:" +
                    $" {string.Join(", ", invalidWords)}");
            }

            return new CreateBaseWordsDTO
            {
                BaseWord = result.Select(w => w.NormalizedWord).ToList(),
                Message = message.Count > 0 ? string.Join(" ", message)
                          : "All words added successfully."
            };
    }
}

