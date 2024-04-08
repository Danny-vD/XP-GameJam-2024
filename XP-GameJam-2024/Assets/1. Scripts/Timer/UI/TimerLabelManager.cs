using FMOD.Studio;
using FMODUtilityPackage.Core;
using FMODUtilityPackage.Enums;
using Timer.Events;
using TMPro;
using UnityEngine;
using VDFramework;
using VDFramework.Utility;

namespace Timer.UI
{
	[RequireComponent(typeof(TMP_Text))]
	public class TimerLabelManager : BetterMonoBehaviour
	{
		// 12 PM ---> 24 PM
		[SerializeField, Tooltip("The shown start time in Hours:Minutes")]
		private Vector2 StartTime = new Vector2(12, 0);

		[SerializeField, Tooltip("The shown time at the end in Hours:Minutes")]
		private Vector2 EndTime = new Vector2(24, 0);

		[SerializeField, Tooltip("The amount of in-game minutes to pass before the timer updates")]
		private int inGameMinutesUpdateInterval = 5;

		[SerializeField]
		private GameTimer gameTimer;

		[SerializeField]
		private TMP_Text[] timerLabels;
		
		private double lastTime;
		
		private StringVariableWriter timerWriter;

		private double startTotalSeconds = 0;
		private double endTotalSeconds = 0;

		private EventInstance playClockTick;

		private void Awake()
		{
			timerWriter = new StringVariableWriter(timerLabels[0].text);

			playClockTick = AudioPlayer.GetEventInstance(AudioEventType.Sound_Effects_Time_clockTick);
		}

		private void Start()
		{
			startTotalSeconds = ToTotalSeconds(StartTime.x, StartTime.y);
			endTotalSeconds   = ToTotalSeconds(EndTime.x, EndTime.y);

			GameTimerExpiredEvent.AddListener(UpdateText);
		}

		private void LateUpdate()
		{
			UpdateText();
		}

		private void UpdateText()
		{
			if (gameTimer.TotalSecondsRemainingNormalized == 0)
			{
				string newText = timerWriter.UpdateText(EndTime.x, EndTime.y);

				foreach (TMP_Text timerLabel in timerLabels)
				{
					timerLabel.text = newText;
				}
			}
			else
			{
				double currentSeconds = Lerp(startTotalSeconds, endTotalSeconds, 1 - gameTimer.TotalSecondsRemainingNormalized);
				ToTime(currentSeconds, out int minutes, out int seconds);
				
				if (currentSeconds - lastTime > inGameMinutesUpdateInterval)
				{
					string newText = timerWriter.UpdateText(minutes, seconds);

					foreach (TMP_Text timerLabel in timerLabels)
					{
						timerLabel.text = newText;
					}
					
					lastTime        = currentSeconds;
					playClockTick.start();
				}
			}
		}

		private static double ToTotalSeconds(double minutes, double seconds)
		{
			return seconds + minutes * 60;
		}

		private static void ToTime(double totalSeconds, out int minutes, out int seconds)
		{
			minutes = (int)(totalSeconds / 60);
			seconds = (int)(totalSeconds % 60);
		}

		private static double Lerp(double a, double b, double t)
		{
			return a + (b - a) * t;
		}

		private void OnDestroy()
		{
			GameTimerExpiredEvent.RemoveListener(UpdateText);
		}
	}
}