using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Talpa_Api.Contexts;
using Talpa_Api.Localization;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class TeamsController(Context context, IStringLocalizer<LocalizationStrings> localizer) : ControllerBase
{
    [HttpGet]
    public ActionResult<Team> GetTeam(string id)
    {
        var team = context.Teams
            .Include(team => team.Users)
            .Include(team => team.Poll)
            .ThenInclude(poll => poll!.Suggestions)
            .Include(team => team.Poll)
            .ThenInclude(poll => poll!.Votes)
            .ToList()
			.Find(team => team.Id == id);
        
        if (team is null) return NotFound(localizer["TeamNotFound"].Value);

        return Ok(team);
    }

    [HttpPost]
    public async Task<ActionResult> PostTeam(string id)
    {
        if (await context.Teams.FindAsync(id) is not null) return Conflict(localizer["TeamAlreadyExists"].Value);
        
        await context.Teams.AddAsync(new Team
        {
            Id = id
        });

        await context.SaveChangesAsync();

        return Created();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteTeam(string id)
    {
        var team = await context.Teams.FindAsync(id);

        if (team is null) return NotFound(localizer["TeamNotFound"].Value);

        context.Teams.Remove(team);
        
        await context.SaveChangesAsync();

        return Ok();
    }
}