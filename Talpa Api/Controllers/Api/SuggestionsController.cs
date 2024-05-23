using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Talpa_Api.Contexts;
using Talpa_Api.Localization;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class SuggestionsController(Context context, IStringLocalizer<LocalizationStrings> localizer) : ControllerBase
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"];
        
    [HttpGet]
    public async Task<ActionResult<List<Suggestion>>> GetSuggestions()
    {
        return await context.Suggestions
            .Include(x => x.Creator)
            .Include(x => x.Tags)
            .ToListAsync();
    }
        
    [HttpPost]
    public async Task<ActionResult<List<SuggestionWithSimilarity>>> CreateSuggestion(string title, string description, string creatorId, IFormFile? image, bool overrideSimilarity = false)
    {
        var user = await context.Users.FindAsync(creatorId);
        if (user is null) return NotFound(localizer["UserNotFound"].Value);
            
        var imagePath = "images/default.png";

        if (image != null)
        {
            if (image.Length > 3 * 1000000) return StatusCode(413, localizer["ImageTooLarge"].Value); // Request Entity Too Large / Payload Too Large (image too big).

            imagePath = await SaveImage(image);

            if (string.IsNullOrEmpty(imagePath))
                return BadRequest(localizer["ImageInvalid"].Value);
        }
            
            
        var (suggestionsWithSimilarity, maxSimilarity) = GetSuggestionsWithSimilarity(title);

        if (maxSimilarity >= 90)
            return Conflict(localizer["SuggestionTooSimilar"].Value);

        if (!overrideSimilarity && suggestionsWithSimilarity.Count > 0 && maxSimilarity > 70) 
            return Accepted(suggestionsWithSimilarity.OrderByDescending(x => x.Similarity).ToList());
            
            
        context.Suggestions.Add(new Suggestion
        {
            Title       = title,
            Description = description,
            ImagePath   = imagePath,
            Creator     = user
        });
        
        await context.SaveChangesAsync();
        
        return Created();
    }

    [HttpPut]
    public async Task<ActionResult> ChangeSuggestion(int id, string description)
    {
        var suggestion = await context.Suggestions.FindAsync(id);

        if (suggestion is null) return NotFound(localizer["SuggestionNotFound"].Value);

        suggestion.Description = description;

        await context.SaveChangesAsync();
        
        return Created();
    }

    private static async Task<string?> SaveImage(IFormFile image)
    {
        var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

        if (string.IsNullOrEmpty(extension) || !AllowedExtensions.Contains(extension) ||
            image.Length > 3 * 1024 * 1024)
        {
            return null;
        }

        var fileName = $"{Guid.NewGuid()}{extension}";
        var path = Path.Combine("wwwroot", "images", fileName);

        await using var stream = new FileStream(path, FileMode.Create);

        await image.CopyToAsync(stream);


        return Path.Combine("images", fileName);
    }

    private (List<SuggestionWithSimilarity>, double) GetSuggestionsWithSimilarity(string title)
    {
        var suggestionsWithSimilarity = new List<SuggestionWithSimilarity>();
        var maxSimilarity = 0.0;
            
        foreach (var sug in context.Suggestions)
        {
            var sim = CalculateSimilarityPercentage(sug.Title, title);
                
            if (sim > maxSimilarity) maxSimilarity = sim;
                
            if (sim > 50) suggestionsWithSimilarity.Add(new SuggestionWithSimilarity(sug.Id, sug.Title, sim));
        }
            
        return (suggestionsWithSimilarity, maxSimilarity);
    }

    private double CalculateSimilarityPercentage(string string1, string string2)
    {
        var pairs1 = WordLetterPairs(string1.ToUpper());
        var pairs2 = WordLetterPairs(string2.ToUpper());

        var intersection = 0;
        var union        = pairs1.Count + pairs2.Count;

        foreach (var pair1 in pairs1)
        {
            for (var number = 0; number < pairs2.Count; number++)
            {
                if (pair1 != pairs2[number]) continue;
                    
                intersection++;
                pairs2.RemoveAt(number);
                break;
            }
        }

        // return the percentage of similarity
        return 2.0 * intersection * 100 / union;
    }

    // Required for the CalculateSimilarityPercentage method.
    private List<string> WordLetterPairs(string str)
    {
        var allPairs = new List<string>();
        var     words    = str.Split(' ');

        foreach (var word in words)
        {
            for (var number = 0; number < word.Length - 1; number++)
            {
                allPairs.Add(word.Substring(number, 2));
            }
        }

        return allPairs;
    }
}