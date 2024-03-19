using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talpa_Api.Contexts;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class ApiController(Context context) : ControllerBase
{
    // GET: api/GetUser
    [HttpGet("{id:int}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = context.Users.Find(id);

        return user == null ? NotFound() : user;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSuggestion(string title, string description, int creatorId)
    {
        var user = await context.Users.FindAsync(creatorId);

        if (user == null)
        {
            return NotFound();
        }
        
        user.Suggestions.Add(new Suggestion
        {
            Title = title,
            Description = description,
            Creator = user
        });

        await context.SaveChangesAsync();

        return NoContent();
    }
}