using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talpa_Api.Contexts;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class UsersController(Context context) : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<User>> GetUser(string id)
    {
        var user = context.Users.Include(user => user.Team).ToList().Find(user => user.Id == id);

        if (user is null) return NotFound("User not found");

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> PostUser(string userId, string teamId)
    {
        var team = await context.Teams.FindAsync(teamId);
            
        if (team is null) return NotFound("Team not found");
        if (await context.Users.FindAsync(userId) is not null) return Conflict("User already exists");

        team.Users.Add(new User
        {
            Id = userId,
            Team = team
        });

        await context.SaveChangesAsync();

        return Created();
    }

    [HttpDelete]

    public async Task<ActionResult> DeleteUser(string userId)
    {
        var user = await context.Users.FindAsync(userId);
        
        if (user is null) return NotFound("User not found");

        context.Users.Remove(user);

        await context.SaveChangesAsync();

        return Ok();
    }
}