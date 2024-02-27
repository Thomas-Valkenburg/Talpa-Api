using Talpa_Api.Context;
using Talpa_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Talpa_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(UserContext context) : ControllerBase
{
    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await context.Users.ToListAsync();
    }

    // GET: api/Users/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUsers(int id)
    {
        var user = await context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // PUT: api/Users/5
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutUsers(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        context.Entry(user).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/Users
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<User>> PostUsers(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }

    // DELETE: api/Users/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUsers(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return context.Users.Any(e => e.Id == id);
    }
}