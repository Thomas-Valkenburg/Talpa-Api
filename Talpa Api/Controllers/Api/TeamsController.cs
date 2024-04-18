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
        
        if (team == null) return NotFound("Team not found");

        return Ok(team);
    }

    [HttpPost]
    public async Task<ActionResult> PostTeam(string id)
    {
        await context.Teams.AddAsync(new Team
        {
            Id = id
        });

        await context.SaveChangesAsync();

        return Created();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteTeam(int id)
    {
        var team = await context.Teams.FindAsync(id);

        if (team == null) return NotFound("Team not found");

        context.Teams.Remove(team);
        
        await context.SaveChangesAsync();

        return Ok();
    }
}