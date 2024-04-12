using System;
using FMODUtilityPackage.Core;
using FMODUtilityPackage.Enums;
using Gameplay.TimeManagement.Events;
using VDFramework;

namespace Gameplay.TimeManagement.EventHandlers.Audio
{
	public class TimeSlowingAudioEffect : BetterMonoBehaviour
	{
		private FMOD.Studio.EventInstance timeSpeedUpEffect;
		private FMOD.Studio.EventInstance timeSlowedEffect;

		private void Awake()
		{
			timeSpeedUpEffect = AudioPlayer.GetEventInstance(AudioEventType.Sound_Effects_Time_TimeSpeedup);
			timeSlowedEffect  = AudioPlayer.GetEventInstance(AudioEventType.Sound_Effects_Time_TimeSlowdown);
		}

		private void OnEnable()
		{
			TimeNormalEvent.AddListener(PlayNormalMusic);
			TimeSlowedEvent.AddListener(PlaySlowedDownMusic);
		}

		private void OnDisable()
		{
			TimeNormalEvent.RemoveListener(PlayNormalMusic);
			TimeSlowedEvent.RemoveListener(PlaySlowedDownMusic);
		}

		private void PlayNormalMusic()
		{
			AudioParameterManager.SetGlobalParameter("TimeSlowed", 0);
			timeSpeedUpEffect.start();
		}

		private void PlaySlowedDownMusic()
		{
			AudioParameterManager.SetGlobalParameter("TimeSlowed", 1);
			timeSlowedEffect.start();
		}

		private void OnDestroy()
		{
			timeSpeedUpEffect.release();
			timeSlowedEffect.release();
		}
	}
}