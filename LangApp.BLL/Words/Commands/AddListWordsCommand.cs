using AutoMapper;
using LangApp.BLL.Exceptions;
using LangApp.BLL.Validation;
using LangApp.BLL.Words.DTOs;
using LangApp.BLL.Words.Services;
using LangApp.Core.Interfaces.Repository;
using LangApp.Core.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace LangApp.BLL.Words.Commands;
// TODO: Replace with batch processing for large datasets
public record AddListWordsCommand(AddWordsByCategoryDTO NewWords) : IRequest<WordsListResponseDTO>;

public class AddListWordsCommandHandler(IBaseWordRepository baseWordRepo, ICategoryRepository categoryRepo, IMapper mapper)
    : IRequestHandler<AddListWordsCommand, WordsListResponseDTO>
{
    public async Task<WordsListResponseDTO> Handle(AddListWordsCommand request, CancellationToken cancellationToken)
    {
        var validWords = new List<CreateBaseWordDTO>();
        var invalidWords = new List<string>();

        if (!TextValidation.IsValidText(request.NewWords.CategoryName))
        { 
            throw new ArgumentException("Category name contains invalid characters.");
        }
        foreach (var word in request.NewWords.Words)
        {
            if (!TextValidation.IsValidText(word.NormalizedWord))
            {
                invalidWords.Add(word.NormalizedWord);
                continue;
            }
            validWords.Add(word);
        }
        var category = await categoryRepo.GetCategoryByNameAsync(request.NewWords.CategoryName);

        if (category == null)
        {
            category = new Category
            {
                Name = request.NewWords.CategoryName.ToLower()
            };

            await categoryRepo.AddCategoryAsync(category);
        }

        var words = mapper.Map<List<BaseWord>>(validWords);
        
        var normalizedWords = words.Select(WordService.NormalizedWord).ToList();

        var existWords = await baseWordRepo.GetAllNormalizedWordsAsync();

        var wordsToInsert = normalizedWords.Where(w => !existWords.Contains(w.NormalizedWord)).ToList();

        var skipedWords = normalizedWords.Where(w => existWords.Contains(w.NormalizedWord))
                                         .Select(w => w.NormalizedWord).ToList();

        if (wordsToInsert.Count == 0)
        {
            throw new ConflictException("Words already exist");
        }

        foreach (var word in wordsToInsert)
        {
            word.Categories.Add(category);
        }
        
        await baseWordRepo.AddListWordsAsync(wordsToInsert);

        return new WordsListResponseDTO
        {
            CategoryName = category.Name,
            Words = [.. wordsToInsert.Select(w => w.NormalizedWord)],
            Message = skipedWords.Count > 0 || invalidWords.Count > 0
            ? $"Some words were skipped because they already exist: {string.Join(", ", skipedWords)}" +
              $"Some words were skipped because they contain invalid characters: {string.Join(", ", invalidWords)}"
            : "All words added successfully."
        };
    }
}
