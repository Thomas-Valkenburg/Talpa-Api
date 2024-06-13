using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Talpa_Api.Contexts;
using Talpa_Api.Localization;
using Talpa_Api.Models;
using Talpa_Api.Algorithms;

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
            .Include(suggestion => suggestion.Creator)
            .Include(suggestion => suggestion.Tags)
            .ToListAsync();
    }
        
    [HttpPost]
    public async Task<ActionResult<List<SimilarityCheck.ObjectWithSimilarity>>> CreateSuggestion(string title, string description, string creatorId, List<int> tagIds, IFormFile? image, bool overrideSimilarity = false)
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
            
        var similarity = SimilarityCheck.GetObjectWithSimilarity(title, context.Suggestions);

        if (similarity.max >= 90)
            return Conflict(localizer["SuggestionTooSimilar"].Value);

        if (!overrideSimilarity && similarity.objects.Count > 0 && similarity.max > 70)
            return Accepted(similarity.objects.OrderByDescending(x => x.Similarity).ToList());

        if (tagIds.Any(tag => context.Tags.Find(tag) is null))
            return NotFound(localizer["TagNotFound"].Value);

        var tags = await context.Tags.Where(tag => tagIds.Contains(tag.Id)).ToListAsync();

        context.Suggestions.Add(new Suggestion
        {
            Title = title,
            Description = description,
            ImagePath = imagePath,
            Creator = user,
            Tags = tags
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
}