using Microsoft.AspNetCore.Mvc;
using Talpa_Api.Contexts;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionsController(Context context) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateSuggestion(string title, string description, int creatorId)
        {
            var user = await context.Users.FindAsync(creatorId);

            if (user == null)
            {
                return NotFound();
            }

            user.Suggestions.Add(new Suggestion
            {
                Title       = title,
                Description = description,
                Creator     = user
            });

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
