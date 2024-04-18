using FMOD.Studio;
using FMODUtilityPackage.Core;
using FMODUtilityPackage.Enums;
using Gameplay.DirectionsSystem.Player;
using MapMovement.Commands.Interfaces;
using UnityEngine;
using VDFramework;

namespace Gameplay.DirectionsSystem.Audio
{
	[RequireComponent(typeof(DirectionGiver))]
	public class GiveDirectionAudioPlayer : BetterMonoBehaviour
	{
		[SerializeField]
		private AudioEventType giveDirectionAudioEvent;

		[SerializeField]
		private AudioEventType removeDirectionAudioEvent;

		private EventInstance addDirectionAudioInstance;
		private EventInstance removeDirectionAudioInstance;

		private DirectionGiver directionGiver;

		private void Awake()
		{
			directionGiver = GetComponent<DirectionGiver>();
		}

		private void Start()
		{
			addDirectionAudioInstance    = AudioPlayer.GetEventInstance(giveDirectionAudioEvent);
			removeDirectionAudioInstance = AudioPlayer.GetEventInstance(removeDirectionAudioEvent);
		}

		private void OnEnable()
		{
			directionGiver.OnDirectionAdded   += PlayAddedSound;
			directionGiver.OnDirectionRemoved += PlayRemovedSound;
		}

		private void OnDisable()
		{
			directionGiver.OnDirectionAdded   -= PlayAddedSound;
			directionGiver.OnDirectionRemoved -= PlayRemovedSound;
		}

		private void PlayAddedSound(AbstractMoveCommand _)
		{
			addDirectionAudioInstance.start();
		}

		private void PlayRemovedSound(AbstractMoveCommand _)
		{
			removeDirectionAudioInstance.start();
		}

		private void OnDestroy()
		{
			addDirectionAudioInstance.release();
			removeDirectionAudioInstance.release();
		}
	}
}