using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Diagnostics;

namespace VisualTimer
{
	internal class MessageSender
	{

		public void SendMessage(TimeSpan timeSpan, string globalTime, Stopwatch _stopWatch)
		{
			timeSpan = _stopWatch.Elapsed;

			string localTime = String.Format("{0:00}:{1:00}:{2:00}",
				timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

			new ToastContentBuilder()
			.AddArgument("action", "Visual Studio Timer")
			.AddText("You're the best =)")
			.AddText($"You worked today for {localTime}")
			.AddText($"General time {globalTime}")
			.Schedule(DateTime.Now.AddSeconds(1));
		}
	}
}
