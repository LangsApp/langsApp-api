using LangApp.BLL.Categories.Queries;
using MediatR;
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
    }
}
