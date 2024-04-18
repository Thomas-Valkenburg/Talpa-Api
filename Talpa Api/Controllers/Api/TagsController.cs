using Microsoft.AspNetCore.Mvc;
using Talpa_Api.Contexts;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class TagsController(Context context) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateTag(string title, bool restrictive, int suggestionId)
    {
        var suggestion = await context.Suggestions.FindAsync(suggestionId);
        if (suggestion is null) return NotFound();
        
        var tag = new Tag
        {
            Title = title,
            Restrictive = restrictive,
            Suggestions = [suggestion]
        };

        await context.Tags.AddAsync(tag);
        await context.SaveChangesAsync();

        return NoContent();
    }
}