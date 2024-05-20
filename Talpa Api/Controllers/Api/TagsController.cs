using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Talpa_Api.Contexts;
using Talpa_Api.Localization;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class TagsController(Context context, IStringLocalizer<LocalizationStrings> localizer) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateTag(string title, bool restrictive, int suggestionId)
    {
        var suggestion = await context.Suggestions.FindAsync(suggestionId);
        if (suggestion is null) return NotFound(localizer["SuggestionNotFound"]);
        
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