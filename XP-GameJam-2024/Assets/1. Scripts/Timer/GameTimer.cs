using Timer.Events;
using UnityEngine;
using VDFramework;
using VDFramework.Utility.TimerUtil;
using VDFramework.Utility.TimerUtil.TimerHandles;

namespace Timer
{
	public class GameTimer : BetterMonoBehaviour
	{
		[SerializeField]
		private float minutes;

		[SerializeField]
		private float seconds;
		
		private TimerHandle timerHandle;

		public double TotalSecondsRemaining => timerHandle.CurrentTime;
		public double TotalSecondsRemainingNormalized => timerHandle.CurrentTimeNormalized;

		private void Start()
		{
			StartNewTimer();
		}

		private double GetStartTime()
		{
			return seconds + minutes * 60;
		}

		private void StartNewTimer()
		{
			timerHandle?.Stop();

			timerHandle = TimerManager.StartNewTimer(GetStartTime(), GameTimerExpiredEvent.RaiseEvent, false);
		}

		private void OnDestroy()
		{
			timerHandle?.Stop();
		}
	}
}