using Microsoft.AspNetCore.Mvc;
using Talpa_Api.Contexts;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class UsersController(Context context) : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);

        if (user == null) return NotFound("User not found");

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> PostUser(string userId, int teamId)
    {
        var team = await context.Teams.FindAsync(teamId);
            
        if (team == null) return NotFound("Team not found");

        team.Users.Add(new User
        {
            Id = userId,
            Team = team
        });

        await context.SaveChangesAsync();

        return Created();
    }

    [HttpDelete]

    public async Task<ActionResult> DeleteUser(int userId)
    {
        var user = await context.Users.FindAsync(userId);
        
        if (user == null) return NotFound("User not found");

        context.Users.Remove(user);

        await context.SaveChangesAsync();

        return Ok();
    }
}