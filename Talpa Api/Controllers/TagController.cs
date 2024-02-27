using Talpa_Api.Context;
using Talpa_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Talpa_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController(TagContext context) : ControllerBase
{
    // GET: api/Tags
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
    {
        return await context.Tags.ToListAsync();
    }

    // GET: api/Tags/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Tag>> GetTags(int id)
    {
        var tag = await context.Tags.FindAsync(id);

        if (tag == null)
        {
            return NotFound();
        }

        return tag;
    }

    // PUT: api/Tags/5
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutTags(int id, Tag tag)
    {
        if (id != tag.Id)
        {
            return BadRequest();
        }

        context.Entry(tag).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TagExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/Tags
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Tag>> PostTags(Tag tag)
    {
        context.Tags.Add(tag);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTags), new { id = tag.Id }, tag);
    }

    // DELETE: api/Tags/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTags(int id)
    {
        var tag = await context.Tags.FindAsync(id);
        if (tag == null)
        {
            return NotFound();
        }

        context.Tags.Remove(tag);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool TagExists(int id)
    {
        return context.Tags.Any(e => e.Id == id);
    }
}