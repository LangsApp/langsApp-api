using LangApp.BLL.Words.Commands;
using LangApp.BLL.Words.DTOs;
using LangApp.Core.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
﻿using Microsoft.AspNetCore.Mvc;


namespace LangApp.API.Controllers;
[Authorize(Roles = UserRoles.SuperAdmin)] 
[Route("api/[controller]")]
[ApiController]
public class AdminWordsController(ISender sender, ILogger<AdminWordsController> _logger) : ControllerBase
{
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
}