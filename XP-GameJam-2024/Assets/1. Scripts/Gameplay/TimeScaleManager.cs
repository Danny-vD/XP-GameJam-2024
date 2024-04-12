using System;
using Gameplay.TimeManagement.Events;
using Input.Events;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay
{
	public class TimeScaleManager : BetterMonoBehaviour
	{
		[SerializeField]
		private float slowedTimeScale = 0.2f;

		private void Start()
		{
			Time.timeScale = 1;
		}

		private void OnEnable()
		{
			SwitchToNormalInputEvent.AddListener(ResumeNormalTime);
			SwitchToSpecialInputEvent.AddListener(SlowDownTime);
		}

		private void OnDisable()
		{
			SwitchToNormalInputEvent.RemoveListener(ResumeNormalTime);
			SwitchToSpecialInputEvent.RemoveListener(SlowDownTime);
		}

		private static void ResumeNormalTime()
		{
			Time.timeScale = 1;
			EventManager.RaiseEvent(new TimeNormalEvent());
		}

		private void SlowDownTime()
		{
			Time.timeScale = slowedTimeScale;
			EventManager.RaiseEvent(new TimeSlowedEvent());
		}
	}
}