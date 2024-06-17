using Talpa_Api.Controllers.Api;
using Talpa_Api.Models;
using Timer = System.Timers.Timer;

namespace Talpa_Api.Events;

public static class PollEndTimer
{
	private static readonly List<Timer> Timers = [];

	public static void CreateTimer(Poll poll)
	{
		switch (poll)
		{
			case { HasEnded: true, HasPointsAssigned: true }:
				return;
			case { HasEnded: true, HasPointsAssigned: false }:
				PollController.AssignPoints(poll.Id);
				return;
			default:
			{
				var timer = new Timer
				{
					Interval = poll.EndDate.Subtract(DateTime.UtcNow).TotalMilliseconds,
					AutoReset = false,
				};

				timer.Elapsed += (_, _) => PollController.AssignPoints(poll.Id);

					timer.Start();

				Timers.Add(timer);
				break;
			}
		}
	}
}