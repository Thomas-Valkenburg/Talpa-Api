using Microsoft.AspNetCore.Mvc;
using Talpa_Api.Contexts;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class CustomizationController(Context context) : ControllerBase
{
	private readonly List<string> AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"];

	[HttpGet]
	public IActionResult GetCustomization()
	{
		return Ok(context.Customization.FirstOrDefault());
	}

	[HttpPost]
	public IActionResult UpdateCustomization(string? name, bool? gradient, string? color1, string? color2, string? color3,
		IFormFile? image)
	{
		var currentCustomization = context.Customization.FirstOrDefault();

		if (image is not null)
		{
			if (!AllowedExtensions.Contains(Path.GetExtension(image.FileName).ToLowerInvariant()))
			{
				return BadRequest();
			}

			var path = Path.Combine("wwwroot", "logo", image.FileName);

			try
			{
				if (!Directory.Exists(Path.Combine("wwwroot", "logo"))) Directory.CreateDirectory(Path.Combine("wwwroot", "logo"));

				var stream = new FileStream(path, FileMode.Create);

				image.CopyTo(stream);
			}
			catch (Exception ex)
			{
				return StatusCode(503, "Image write unavailable: " + ex);
			}
		}

		if (currentCustomization is null)
		{
			if (!gradient.HasValue || color1 is null || image is null ||
			    (gradient is true && (color2 is null || color3 is null)))
			{
				return BadRequest();
			}

			var customization = new Customization(name ?? "Talpa", gradient!.Value, color1, color2, color3, Path.Combine(HttpContext.Request.Host.Value, "logo", image.FileName));

			context.Customization.Add(customization);
			context.SaveChanges();

			return Created();
		}

		if (name is not null)
		{
			currentCustomization.Name = name;
		}

		if (gradient is not null)
		{
			currentCustomization.Gradient = gradient.Value;
		}

		if (color1 is not null)
		{
			currentCustomization.Color1 = color1;
		}

		if (color2 is not null)
		{
			currentCustomization.Color2 = color2;
		}

		if (color3 is not null)
		{
			currentCustomization.Color3 = color3;
		}

		if (image is not null)
		{
			currentCustomization.LogoPath = Path.Combine(HttpContext.Request.Host.Value, "logo", image.FileName);
		}

		context.Customization.Update(currentCustomization);
		context.SaveChanges();

		return NoContent();
	}
}