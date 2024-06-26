using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Poll")]
public class Poll
{
	public Poll(DateTime endDate, List<DateTime> dates, Team team)
	{
        EndDate = endDate;
        Dates   = dates.Select(date => new PollDate {Date = date, Poll = this}).ToList();
        Team = team;
	}

	public Poll(DateTime endDate, List<DateTime> dates, List<Suggestion> suggestions, Team team)
	{
		EndDate     = endDate;
		Dates       = dates.Select(date => new PollDate { Date = date, Poll = this }).ToList();
		Suggestions = suggestions;
		Team        = team;
	}

	public Poll() { }

	[Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public DateTime EndDate { get; init; }

	public bool HasEnded => DateTime.UtcNow > EndDate;

	public bool HasPointsAssigned { get; set; }

    public List<PollDate> Dates { get; init; }

    public Team Team { get; init; }

    public List<Suggestion> Suggestions { get; init; } = [];

    public List<Vote> Votes { get; init; } = [];
}