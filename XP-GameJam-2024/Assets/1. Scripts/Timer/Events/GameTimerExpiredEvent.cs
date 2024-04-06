using VDFramework.EventSystem;

namespace Timer.Events
{
	public class GameTimerExpiredEvent : VDEvent<GameTimerExpiredEvent>
	{
		public static void RaiseEvent()
		{
			EventManager.RaiseEvent(new GameTimerExpiredEvent());
		}
	}
}