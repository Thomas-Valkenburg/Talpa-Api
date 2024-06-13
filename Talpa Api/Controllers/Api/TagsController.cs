using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Talpa_Api.Algorithms;
using Talpa_Api.Contexts;
using Talpa_Api.Localization;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class TagsController(Context context, IStringLocalizer<LocalizationStrings> localizer) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<List<SimilarityCheck.ObjectWithSimilarity>>> CreateTag(string title, bool restrictive, bool overrideSimilarity = false)
    {
        var similarity = SimilarityCheck.GetObjectWithSimilarity(title, context);

        if (similarity.max >= 90) return Conflict(localizer["TagAlreadyExists"].Value);

        if (similarity.objects.Count > 0 && similarity.max > 70 && !overrideSimilarity)
            return Accepted(similarity.objects.OrderByDescending(x => x.Similarity).ToList());
        
        var tag = new Tag
        {
            Title = title,
            Restrictive = restrictive,
            Suggestions = []
        };

        await context.Tags.AddAsync(tag);
        await context.SaveChangesAsync();

        return NoContent();
    }
}