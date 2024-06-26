using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Talpa_Api.Contexts;
using Talpa_Api.Localization;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class VotesController(Context context, IStringLocalizer<LocalizationStrings> localizer) : ControllerBase
{
        
    [HttpGet]
    public async Task<ActionResult<VotesWithCount>> GetVotesFromPollId(int pollId)
    {
        var polls = await context.Polls
            .Where(x => x.Id == pollId)
            .Include(poll => poll.Votes)
            .ThenInclude(x => x.Creator)
            .Include(x => x.Votes)
            .ThenInclude(x => x.Suggestion)
            .ToListAsync();

        if (polls.Count < 1) return NotFound(localizer["PollNotFound"].Value);

        var votes = polls.First().Votes;

        // Calculate votes per suggestion
        var votesPerSuggestion = new Dictionary<int, int>();
        foreach (var vote in votes)
        {
            var suggestionId = vote.Suggestion.Id;
            if (!votesPerSuggestion.TryAdd(suggestionId, 1))
            {
                votesPerSuggestion[suggestionId]++;
            }
        }

        return Ok(new VotesWithCount
        {
            Votes = votes,
            VotesPerSuggestion = votesPerSuggestion
        });
    }

    [HttpPost]
    public async Task<ActionResult> CreateVote(string userId, int pollId, int suggestionId, List<int> pollDates)
    {
        var user = context.Users
            .Include(x => x.Votes)
            .ThenInclude(vote => vote.Poll)
            .ToList()
            .Find(x => x.Id == userId);
            
        var poll = await context.Polls.FindAsync(pollId);

        var suggestion = context.Suggestions
	        .Include(suggestion => suggestion.Creator)
	        .ToList()
	        .Find(x => x.Id == suggestionId);

        var dates = context.PollDates
			.Where(x => pollDates.Contains(x.Id))
			.ToList();
            
        if (user       is null) return NotFound(localizer["UserNotFound"].Value);
        if (poll       is null) return NotFound(localizer["PollNotFound"].Value);
        if (suggestion is null) return NotFound(localizer["SuggestionNotFound"].Value);
        if (poll.HasEnded) return Conflict(localizer["PollHasEnded"].Value);
            
        if (user.Votes.Any(x => x.Poll.Id == pollId)) return Conflict(localizer["UserAlreadyVoted"].Value);
            
        await context.Votes.AddAsync(new Vote
        {
            Creator = user,
            Poll = poll,
            Suggestion = suggestion,
            Dates = dates
        });

        if (suggestion.Creator is not null) suggestion.Creator.Points += 1;
            
        await context.SaveChangesAsync();
        return Created();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteVote(int voteId)
    {
        var vote = await context.Votes.FindAsync(voteId);
        if (vote is null) return NotFound(localizer["VoteNotFound"].Value);
            
        context.Votes.Remove(vote);

        await context.SaveChangesAsync();
        return NoContent();
    }
}
