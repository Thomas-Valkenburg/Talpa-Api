using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talpa_Api.Contexts;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class PollController(Context context) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreatePoll(string teamId, DateOnly endDate, List<int> suggestionIds)
    {
        var team = context.Teams.Include(team => team.Poll).ToList().Find(x => x.Id == teamId);
        
        if (team is null) return NotFound("Team not found.");
        if (team.Poll is not null && team.Poll.EndDate <= DateOnly.FromDateTime(DateTime.Now)) 
            return Conflict("Team already has a poll.");
        if (suggestionIds.Count != 3) return BadRequest("Poll must have exactly 3 suggestions.");
        if (suggestionIds.Any(id => context.Suggestions.Find(id) is null)) return NotFound("One or more suggestions not found.");
        
        team.Poll = new Poll
        {
            EndDate = endDate,
            Suggestions = suggestionIds.Select(id => context.Suggestions.Find(id)).ToList()!,
            Team = team
        };
        
        await context.SaveChangesAsync();
        
        return Created();
    }
}