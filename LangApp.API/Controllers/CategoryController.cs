using LangApp.BLL.Categories.Commands;
using LangApp.BLL.Categories.DTOs;
using LangApp.BLL.Categories.Queries;
using LangApp.Core.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LangApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ISender sender, ILogger<CategoryController> logger) : ControllerBase
    {
        [HttpGet("get-categories")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            logger.LogInformation("Received request to get categories.");
            var result = await sender.Send(new GetCategoriesQuery());
            if (result is null)
            {
                logger.LogError("Failed to get categories.");
                return BadRequest("Failed to get categories.");
            }
            logger.LogInformation("Successfully retrieved categories.");
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CreateCategoryDTO newCategory)
        {
            logger.LogInformation("Received request to add a new category: {CategoryName}", newCategory.Name);
            var result = await sender.Send(new CreateCategoryCommand(newCategory));
            
            if(result is null)
            {
                logger.LogError("Failed to add category: {CategoryName}", newCategory.Name);
                return BadRequest("Failed to add category.");
            }
            logger.LogInformation("Successfully added category: {CategoryName}", newCategory.Name);
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryDTO updatedCategory)
        {
            logger.LogInformation($"Received request to update category: {updatedCategory.CategoryToEditName}");
            var result = await sender.Send(new UpdateCategoryCommand(updatedCategory));

            if(result is null)
            {
                logger.LogError($"Failed to edit category: {updatedCategory.CategoryToEditName}");
                return BadRequest("Failed to edit category");
            }
            logger.LogInformation($"Successfully updated category: {updatedCategory.EditedCategory}");
            return Ok(result);
        }
    }
}
