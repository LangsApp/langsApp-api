using AutoMapper;
using LangApp.BLL.Exceptions;
using LangApp.BLL.Words.DTOs;
using LangApp.BLL.Words.Service;
using LangApp.Core.Interfaces;
using LangApp.Core.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace LangApp.BLL.Words.Commands;
// TODO: Replace with batch processing for large datasets
public record AddListWordsCommand(AddWordsByCategoryDTO NewWords) : IRequest<WordsListResponseDTO>;

public class AddListWordsCommandHandler(IBaseWord baseWordRepo, ICategory categoryRepo, IMapper mapper)
    : IRequestHandler<AddListWordsCommand, WordsListResponseDTO>
{
    public async Task<WordsListResponseDTO> Handle(AddListWordsCommand request, CancellationToken cancellationToken)
    {
        if (request.NewWords.CategoryName.IsNullOrEmpty())
        {
            throw new ArgumentException("Category name cannot be null or empty.");
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

        var words = mapper.Map<List<BaseWord>>(request.NewWords.Words);
        
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
            Message = skipedWords.Count > 0
            ? $"Some words were skipped because they already exist: {string.Join(", ", skipedWords)}"
            : "All words added successfully."
        }; ;
    }
}
