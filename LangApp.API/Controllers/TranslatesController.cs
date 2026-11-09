using LangApp.BLL.Categories.Commands;
using LangApp.BLL.DTOs;
using LangApp.BLL.Translations.Commands;
using LangApp.BLL.Translations.DTOs;
using LangApp.BLL.Translations.Queries;
using LangApp.Core.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LangApp.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TranslatesController(ISender sender, ILogger<TranslatesController> logger) : ControllerBase
{
    [Authorize(Roles = UserRoles.SuperAdmin)]
    [HttpPost("create-list-translates")]
    public async Task<IActionResult> CreateListTranslatesAsync()
    {
        var result = await sender.Send(new CreateListTranslatesCommand());
        if (result is null)
        {
            logger.LogError("Failed to create list of translates.");
            return BadRequest("Failed to create list of translates.");
        }
        logger.LogInformation(result.Message);
        return Ok(result);
    }

    [HttpGet("get-all-translations")]
    public async Task<IActionResult> GetTranslationsAsync()
    {
        logger.LogInformation("Received request to get available translates.");
        var result = await sender.Send(new GetTranslationsQuery());

        if(result is null)
        {
            logger.LogError("Failed to get available translates.");
            return BadRequest("Failed to get available translates.");
        }

        logger.LogInformation("Successfully retrieved available translates.");
        return Ok(result);
    }

    [Authorize(Roles = UserRoles.SuperAdmin)]
    [HttpPut("update-translate")]
    public async Task<IActionResult> UpdateTranslateAsync([FromBody] UpdateTranslateDTO updateTranslateDTO)
    {
        logger.LogInformation($"Recived request to update translate: {updateTranslateDTO.TranslateToEdit}");
        var result = await sender.Send(new UpdateTranslateCommand(updateTranslateDTO));
        if(result is null)
        {
            logger.LogError($"Failed to edit translate: {updateTranslateDTO.TranslateToEdit}");
            return BadRequest("Failed to edit translate");
        }
        logger.LogInformation($"Successfully updated translate: {updateTranslateDTO.EditedTranslate}");
        return Ok(result);
    }

    [Authorize(Roles = UserRoles.SuperAdmin)]
    [HttpDelete("delete-translate/{id}")]
    public async Task<IActionResult> DeleteTranslateAsync([FromRoute] Guid id)
    {
        logger.LogInformation($"Receiced request to delete translate: {id}");
        var result = await sender.Send(new DeleteTranslateCommand(id));

        if (result is null)
        {
            logger.LogError($"Failed to delete translate: {id}");
            return BadRequest("Failed to delete translate");
        }
        logger.LogInformation($"Successfully deleted translate: {id}");
        return Ok(result);
    }

    [Authorize(Roles = UserRoles.SuperAdmin)]
    [HttpPut("publish-translates")]
    public async Task<IActionResult> PublishTranslatesAsync([FromBody] List<ReviewAndPublishDTO> reviewAndPublishDTO)
    {
        logger.LogInformation("Received request to publish translates");
        var result = await sender.Send(new ReviewOrPublishTranslateCommand(reviewAndPublishDTO));

        if(result is null)
        {
            logger.LogError("Failed to update status of translates");
            return BadRequest("Failed to update status translates");
        }
        logger.LogInformation("Successfully updated status of translates");
        return Ok(result);
    }
}
