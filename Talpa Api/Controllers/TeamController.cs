using Talpa_Api.Context;
using Talpa_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Talpa_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController(TeamContext context) : ControllerBase
{
    // GET: api/Teams
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
    {
        return await context.Teams.ToListAsync();
    }

    // GET: api/Teams/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Team>> GetTeams(int id)
    {
        var team = await context.Teams.FindAsync(id);

        if (team == null)
        {
            return NotFound();
        }

        return team;
    }

    // PUT: api/Teams/5
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutTeams(int id, Team team)
    {
        if (id != team.Id)
        {
            return BadRequest();
        }

        context.Entry(team).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TeamExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/Teams
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Team>> PostTeams(Team team)
    {
        context.Teams.Add(team);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTeams), new { id = team.Id }, team);
    }

    // DELETE: api/Teams/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTeams(int id)
    {
        var team = await context.Teams.FindAsync(id);
        if (team == null)
        {
            return NotFound();
        }

        context.Teams.Remove(team);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool TeamExists(int id)
    {
        return context.Teams.Any(e => e.Id == id);
    }
}