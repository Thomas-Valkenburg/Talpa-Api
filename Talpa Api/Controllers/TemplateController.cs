﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talpa_Api.Contexts;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers;

#if DEBUG

[Route("api/[controller]")]
[ApiController]
public class TemplateController(Context context) : ControllerBase
{
    // GET: api/Suggestions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Suggestion>>> GetSuggestions()
    {
        return await context.Suggestions
            .AsNoTracking() // Don't include classes that are not 'Included'.
            .Include(suggestion => suggestion.Creator) // Include the creator of the suggestion.
            .ThenInclude(user => user.Team) // Include the team of the creator of the suggestion.
            .Include(suggestion => suggestion.Tags) // Include the tags of the suggestion.
            .ToListAsync();
    }

    // GET: api/Suggestions/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Suggestion>> GetSuggestions(int id)
    {
        var suggestion = await context.Suggestions.FindAsync(id);

        if (suggestion == null)
        {
            return NotFound();
        }

        return suggestion;
    }

    // PUT: api/Suggestions/5
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutSuggestions(int id, Suggestion suggestion)
    {
        if (id != suggestion.Id)
        {
            return BadRequest();
        }

        context.Entry(suggestion).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SuggestionExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/Suggestions
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Suggestion>> PostSuggestions(Suggestion suggestion)
    {
        context.Suggestions.Add(suggestion);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSuggestions), new { id = suggestion.Id }, suggestion);
    }

    // DELETE: api/Suggestions/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSuggestions(int id)
    {
        var suggestion = await context.Suggestions.FindAsync(id);
        if (suggestion == null)
        {
            return NotFound();
        }

        context.Suggestions.Remove(suggestion);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool SuggestionExists(int id)
    {
        return context.Suggestions.Any(e => e.Id == id);
    }
}

#endif