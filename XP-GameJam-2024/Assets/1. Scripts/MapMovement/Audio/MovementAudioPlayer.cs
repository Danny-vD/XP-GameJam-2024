using FMOD.Studio;
using FMODUtilityPackage.Core;
using FMODUtilityPackage.Enums;
using MapMovement.Interfaces;
using UnityEngine;
using VDFramework;

namespace MapMovement.Audio
{
	[RequireComponent(typeof(IActorMover))]
	public class MovementAudioPlayer : BetterMonoBehaviour
	{
		[SerializeField]
		private AudioEventType audioEventToPlay;
		
		private EventInstance instanceToPlay;

		private IActorMover actorMover;

		private bool isPlaying = false;

		private void Awake()
		{
			actorMover = GetComponent<IActorMover>();
		}

		private void Start()
		{
			instanceToPlay = AudioPlayer.GetEventInstance(audioEventToPlay);
		}

		private void OnEnable()
		{
			actorMover.OnMovementStart   += StartPlaying;
			actorMover.OnMovementStopped += StopPlaying;
		}

		private void OnDisable()
		{
			actorMover.OnMovementStart   -= StartPlaying;
			actorMover.OnMovementStopped -= StopPlaying;
		}

		private void StartPlaying()
		{
			if (!isPlaying)
			{
				instanceToPlay.start();
			}
			
			isPlaying = true;
			
			instanceToPlay.setPaused(false);
		}

		private void StopPlaying()
		{
			instanceToPlay.setPaused(true);
		}

		private void OnDestroy()
		{
			isPlaying = false;
			instanceToPlay.stop(STOP_MODE.ALLOWFADEOUT);
			instanceToPlay.release();
		}
	}
}