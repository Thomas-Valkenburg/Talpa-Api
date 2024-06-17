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
	public class SuggestionData(Suggestion suggestion)
	{
		public int Id { get; } = suggestion.Id;

        public string Title { get; } = suggestion.Title;

        public string Description { get; } = suggestion.Description;

        public string ImagePath { get; } = suggestion.ImagePath;

        public SuggestionCreatorData? Creator { get; } = suggestion.Creator is null ? null : new SuggestionCreatorData(suggestion.Creator);

        public List<SuggestionTagData> Tags { get; } = suggestion.Tags.Select(tag => new SuggestionTagData(tag)).ToList();

        public class SuggestionCreatorData(User user)
        {
            public string Id { get; } = user.Id;

            public int Points { get; } = user.Points;
        }

        public class SuggestionTagData(Tag tag)
		{
			public int Id { get; } = tag.Id;

			public string Title { get; } = tag.Title;

			public bool Restrictive { get; } = tag.Restrictive;
		}
	}

	public class SuggestionFormData
	{
		public List<int>?  Tags { get; set; }
		public IFormFile? Image { get; set; }
	}

    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"];

    [HttpGet]
    public async Task<ActionResult<List<SuggestionData>>> GetSuggestions()
    {
        var suggestions = await context.Suggestions
			.Include(suggestion => suggestion.Creator)
			.Include(suggestion => suggestion.Tags)
			.ToListAsync();

        return Ok(suggestions.Select(suggestion => new SuggestionData(suggestion)));
    }
        
    [HttpPost]
    public async Task<ActionResult<List<SimilarityCheck.ObjectWithSimilarity>>> CreateSuggestion(string title, string description, string creatorId, [FromForm] SuggestionFormData formData, bool overrideSimilarity = false)
    {
        var user = await context.Users.FindAsync(creatorId);
        if (user is null) return NotFound(localizer["UserNotFound"].Value);
            
        var imagePath = "images/default.png";

        if (formData.Image != null)
        {
            if (formData.Image.Length > 3 * 1000000) return StatusCode(413, localizer["ImageTooLarge"].Value); // Request Entity Too Large / Payload Too Large (image too big).

            imagePath = await SaveImage(formData.Image);

            if (string.IsNullOrEmpty(imagePath))
                return BadRequest(localizer["ImageInvalid"].Value);
        }
            
        var (objects, max) = SimilarityCheck.GetObjectWithSimilarity(title, context.Suggestions);

        if (max >= 90)
            return Conflict(localizer["SuggestionTooSimilar"].Value);

        if (!overrideSimilarity && objects.Count > 0 && max > 70)
            return Accepted(objects.OrderByDescending(x => x.Similarity).ToList());

        List<Tag> tags = [];

        if (formData.Tags is not null)
        {
	        if (formData.Tags.Any(tag => context.Tags.Find(tag) is null))
		        return NotFound(localizer["TagNotFound"].Value);

	        tags = await context.Tags.Where(tag => formData.Tags.Contains(tag.Id)).ToListAsync();
        }

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