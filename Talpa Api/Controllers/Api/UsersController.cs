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

        if (user == null) return NotFound();

        return user;
    }

    [HttpPost]
    public async Task<ActionResult> PostUser(int teamId, string name, bool isManager)
    {
        var team = await context.Teams.FindAsync(teamId);
            
        if (team == null) return NotFound();
            
        team.Users.Add(new User
        {
            Name      = name,
            IsManager = isManager,
            Team      = team
        });

        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete]

    public async Task<ActionResult> DeleteUser(int userId)
    {
        var user = await context.Users.FindAsync(userId);
        
        if (user == null) return NotFound();

        context.Users.Remove(user);

        await context.SaveChangesAsync();

        return NoContent();
    }
}