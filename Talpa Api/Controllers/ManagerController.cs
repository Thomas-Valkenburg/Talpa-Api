using Talpa_Api.Context;
using Talpa_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Talpa_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManagerController(ManagerContext context) : ControllerBase
{
    // GET: api/Managers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Manager>>> GetManagers()
    {
        return await context.Managers.ToListAsync();
    }

    // GET: api/Managers/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Manager>> GetManagers(int id)
    {
        var manager = await context.Managers.FindAsync(id);

        if (manager == null)
        {
            return NotFound();
        }

        return manager;
    }

    // PUT: api/Managers/5
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutManagers(int id, Manager manager)
    {
        if (id != manager.Id)
        {
            return BadRequest();
        }

        context.Entry(manager).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ManagerExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/Managers
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Manager>> PostManagers(Manager manager)
    {
        context.Managers.Add(manager);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetManagers), new { id = manager.Id }, manager);
    }

    // DELETE: api/Managers/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteManagers(int id)
    {
        var manager = await context.Managers.FindAsync(id);
        if (manager == null)
        {
            return NotFound();
        }

        context.Managers.Remove(manager);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool ManagerExists(int id)
    {
        return context.Managers.Any(e => e.Id == id);
    }
}