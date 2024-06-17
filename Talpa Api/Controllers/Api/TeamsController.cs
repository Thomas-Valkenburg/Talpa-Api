using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Talpa_Api.Contexts;
using Talpa_Api.Localization;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class TeamsController(Context context, IStringLocalizer<LocalizationStrings> localizer) : ControllerBase
{
	public readonly struct TeamData(Team team)
	{
        public string Id { get; } = team.Id;

        public List<TeamUserData> Users { get; } = team.Users.Select(user => new TeamUserData(user)).ToList();

        public TeamPollData? Poll { get; } = team.Poll is null ? null : new TeamPollData(team.Poll);

        public readonly struct TeamUserData(User user)
		{
			public string Id { get; } = user.Id;

			public int Points { get; } = user.Points;
		}

        public readonly struct TeamPollData(Poll poll)
        {
            public int Id { get; } = poll.Id;

            public DateTime EndDateTime { get; } = poll.EndDate;

            public List<TeamPollSuggestionData> Suggestions { get; } = poll.Suggestions.Select(suggestion => new TeamPollSuggestionData(suggestion)).ToList();

            public List<TeamPollDateData> Dates { get; } = poll.Dates.Select(date => new TeamPollDateData(date)).ToList();

            public List<TeamPollVoteData> Votes { get; } = poll.Votes.Select(vote => new TeamPollVoteData(vote)).ToList();

            public readonly struct TeamPollSuggestionData(Suggestion suggestion)
			{
				public int Id { get; } = suggestion.Id;

				public string Title { get; } = suggestion.Title;

				public int Votes { get; } = suggestion.Votes.Count;
			}

			public readonly struct TeamPollDateData(PollDate date)
			{
				public int Id { get; } = date.Id;

				public DateTime Date { get; } = date.Date;

                public int? Votes { get; } = date.Votes?.Count(vote => vote.Dates.Contains(date)) ?? null;
			}

            public readonly struct TeamPollVoteData(Vote vote)
			{
				public int Id { get; } = vote.Id;

                public string CreatorId { get; } = vote.Creator.Id;

                public int SuggestionId { get; } = vote.Suggestion.Id;

				public List<int> Dates { get; } = vote.Dates.Select(date => date.Id).ToList();
			}
        }
	}

    [HttpGet]
    public ActionResult<TeamData> GetTeam(string id)
    {
	    var team = context.Teams
		    .Include(team => team.Users)
		    .Include(team => team.Poll)
		    .ThenInclude(poll => poll!.Suggestions)
		    .Include(team => team.Poll)
		    .ThenInclude(poll => poll!.Dates)
		    .Include(team => team.Poll)
		    .ThenInclude(poll => poll!.Votes)
		    .ThenInclude(vote => vote.Dates)
		    .ToList()
		    .Find(team => team.Id == id);
        
        if (team is null) return NotFound(localizer["TeamNotFound"].Value);

        return Ok(new TeamData(team));
    }

    [HttpPost]
    public async Task<ActionResult> PostTeam(string id)
    {
        if (await context.Teams.FindAsync(id) is not null) return Conflict(localizer["TeamAlreadyExists"].Value);
        
        await context.Teams.AddAsync(new Team
        {
            Id = id
        });

        await context.SaveChangesAsync();

        return Created();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteTeam(string id)
    {
        var team = await context.Teams.FindAsync(id);

        if (team is null) return NotFound(localizer["TeamNotFound"].Value);

        context.Teams.Remove(team);
        
        await context.SaveChangesAsync();

        return Ok();
    }
}