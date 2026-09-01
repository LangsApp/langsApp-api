using LangApp.BLL.Words.Commands;
using LangApp.BLL.Words.DTOs;
using LangApp.BLL.Words.Queries;
using LangApp.Core.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LangApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WordsController(ISender sender, ILogger<WordsController> _logger) : ControllerBase
{
    [Authorize(Roles = UserRoles.SuperAdmin)]
    [HttpPost("add-word")]
    public async Task<IActionResult> AddWordAsync([FromBody] CreateBaseWordDTO newWord)
    {
        var result = await sender.Send(new CreateBaseWordCommand(newWord));
        if (result is null)
        {
            _logger.LogError("Failed to create a new word with normalized word: {NormalizedWord}", 
                newWord.NormalizedWord);
            return BadRequest("Failed to create a new word.");
        }
        _logger.LogInformation("Successfully created a new word with normalized word: {NormalizedWord}", 
            newWord.NormalizedWord);
        return Ok(result);
    }

    [Authorize(Roles = UserRoles.SuperAdmin)]
    [HttpPost("add-list-words")]
    public async Task<IActionResult> AddListWordsByCategoryAsync([FromBody] AddWordsByCategoryDTO newWords)
    {
        _logger.LogInformation($"Attempting to add words for category: {newWords.CategoryName} " +
            $"with {newWords.Words.Count()} words",
            newWords.CategoryName, newWords.Words.Count);

        var result = await sender.Send(new AddListWordsCommand(newWords));
            if (result is null)
        {
            _logger.LogError("Failed to add words for category: {CategoryName}", newWords.CategoryName);
            return BadRequest("Failed to add words for the specified category.");
        }
        _logger.LogInformation($"Words processed for category: {result.CategoryName}. Added: {result.Message}");
        return Ok(result);
    }

    [HttpGet("get-list-words")]
    public async Task<IActionResult> GetBaseWordsAsync()
    {
        _logger.LogInformation("Received request to get available base words.");

        var result = await sender.Send(new GetAllBaseWordsQuery());

        if (result is null)
        { 
            _logger.LogError("Failed to get available base words.");
            return BadRequest("Failed to get available base words.");
        }

        _logger.LogInformation("Successfully retrieved available base words.");
        return Ok(result);
    }
}