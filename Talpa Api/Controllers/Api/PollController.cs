﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Talpa_Api.Contexts;
using Talpa_Api.Localization;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class PollController(Context context, IStringLocalizer<LocalizationStrings> localizer) : ControllerBase
{
	public readonly struct PollData
	{
		public List<DateTime> Dates { get; init; }

		public List<int> SuggestionsIds { get; init; }
	}

	[HttpPost]
    public async Task<ActionResult> CreatePoll(string teamId, DateTime endDate, [FromBody] PollData data)
    {
        var team = context.Teams.Include(team => team.Poll).ToList().Find(x => x.Id == teamId);

        if (team is null)
            return NotFound(localizer["TeamNotFound"].Value);
        if (team.Poll is not null && team.Poll.EndDate > DateTime.Now)
            return Conflict(localizer["TeamAlreadyActivePoll"].Value);
        if (data.Dates.Count < 1)
	        return BadRequest(localizer["CreatePollDateCountWrong"].Value);
        if (data.SuggestionsIds.Count is < 1 or > 3)
            return BadRequest(localizer["CreatePollSuggestionCountWrong"].Value);
        if (data.SuggestionsIds.Any(id => context.Suggestions.Find(id) is null))
            return NotFound(localizer["SuggestionNotFound"].Value);

        team.Poll = new Poll(endDate, data.Dates, data.SuggestionsIds.Select(id => context.Suggestions.Find(id)).ToList()!, team);

        await context.SaveChangesAsync();

        return Created();
    }
}