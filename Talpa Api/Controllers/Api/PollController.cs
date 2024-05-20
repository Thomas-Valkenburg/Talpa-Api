using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talpa_Api.Contexts;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class PollController(Context context) : ControllerBase
{
	public struct PollData
	{
		public List<DateTime> Dates { get; set; }

		public List<int> SuggestionsIds { get; set; }
	}

	[HttpPost]
    public async Task<ActionResult> CreatePoll(string teamId, DateTime endDate, [FromBody] PollData data)
    {
        var team = context.Teams.Include(team => team.Poll).ToList().Find(x => x.Id == teamId);

        if (team is null) 
            return NotFound("Team not found.");
        if (team.Poll is not null && team.Poll.EndDate > DateTime.Now)
            return Conflict("Team already has an active poll.");
        if (data.SuggestionsIds.Count is < 1 or > 3)
            return BadRequest("Poll must have a minimum of 1 suggestion and a maximum of 3.");
        if (data.SuggestionsIds.Any(id => context.Suggestions.Find(id) is null))
            return NotFound("One or more suggestions not found.");

        team.Poll = new Poll(endDate, data.Dates, data.SuggestionsIds.Select(id => context.Suggestions.Find(id)).ToList()!, team);

        await context.SaveChangesAsync();

        return Created();
    }
}