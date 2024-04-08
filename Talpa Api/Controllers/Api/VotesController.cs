using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talpa_Api.Contexts;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers.Api
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController(Context context) : ControllerBase
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

            if (polls.Count < 1) return NotFound("poll not found");

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
        public async Task<ActionResult> CreateVote(int userId, int pollId, int suggestionId)
        {
            var user = context.Users
                .Include(x => x.Votes)
                .ThenInclude(vote => vote.Poll)
                .ToList()
                .Find(x => x.Id == userId);
            
            var poll = await context.Polls.FindAsync(pollId);
            
            var suggestion = await context.Suggestions.FindAsync(suggestionId);
            
            if (user == null) return NotFound("User not found");
            if (poll == null) return NotFound("Poll not found");
            if (suggestion == null) return NotFound("Suggestions not found");
            
            if (user.Votes.Any(x => x.Poll.Id == pollId)) return BadRequest("User already voted in this poll");
            
            await context.Votes.AddAsync(new Vote
            {
                Creator = user,
                Poll = poll,
                Suggestion = suggestion
            });
            
            await context.SaveChangesAsync();

            return Created();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteVote(int voteId)
        {
            var vote = await context.Votes.FindAsync(voteId);
        
            if (vote == null) return NotFound("Vote not found");
            
            context.Votes.Remove(vote);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}