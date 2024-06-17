using Microsoft.AspNetCore.Mvc;
using Talpa_Api.Contexts;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class CustomizationController(Context context) : ControllerBase
{
	[HttpGet]
	public IActionResult GetCustomization()
	{
		return Ok(context.Customization.FirstOrDefault());
	}

	[HttpPost]
	public IActionResult UpdateCustomization([FromBody] Customization customization)
	{
		var currentCustomization = context.Customization.FirstOrDefault();

		if (currentCustomization is null)
		{
			context.Customization.Add(customization);
			return Created();
		}

		context.Customization.Update(customization);
		context.SaveChanges();

		return NoContent();
	}
}