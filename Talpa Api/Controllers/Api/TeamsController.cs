using Microsoft.AspNetCore.Mvc;
using Talpa_Api.Contexts;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class TeamsController(Context context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Team>> GetTeam(int id)
    {
        var team = await context.Teams.FindAsync(id);
        
        if (team == null) return NotFound();

        return team;
    }

    [HttpPost]
    public async Task<ActionResult> PostTeam(string name)
    {
        await context.Teams.AddAsync(new Team
        {
            Name = name
        });

        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> ChangeTeam(int id, string name)
    {
        var team = await context.Teams.FindAsync(id);

        if (team == null) return NotFound();
        
        team.Name = name;

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteTeam(int id)
    {
        var team = await context.Teams.FindAsync(id);

        if (team == null) return NotFound();

        context.Teams.Remove(team);
        
        await context.SaveChangesAsync();

        return NoContent();
    }
}