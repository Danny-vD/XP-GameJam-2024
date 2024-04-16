using System.Collections.Generic;
using FMOD.Studio;
using FMODUtilityPackage.Core;
using FMODUtilityPackage.Enums;
using MapMovement.Commands.Interface;
using MapMovement.NPCs;
using UnityEngine;
using VDFramework;

namespace Gameplay.DirectionsSystem.NPCs.Effects
{
	[RequireComponent(typeof(DirectionsReceiver))]
	public class CanGiveDirectionsEffect : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject objectToEnable;

		[SerializeField]
		private AudioEventType canGiveDirectionsSound = AudioEventType.Sound_Effects_NPCs_npcAlertBeep;

		private EventInstance canGiveDirectionsSoundInstance;

		private DirectionsReceiver directionsReceiver;
		private VillagerController villagerController;

		private void Awake()
		{
			directionsReceiver  = GetComponent<DirectionsReceiver>();
			villagerController  = GetComponent<VillagerController>();

			canGiveDirectionsSoundInstance = AudioPlayer.GetEventInstance(canGiveDirectionsSound);
		}

		private void OnEnable()
		{
			villagerController.OnMovementStart += DisableEffects;
		}

		private void OnDisable()
		{
			villagerController.OnMovementStart -= DisableEffects;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer != 6) // 6 == player layer
			{
				return;
			}

			villagerController.OnMovementStopped += EnableEffectsIfPossible;
			
			EnableEffectsIfPossible();
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.layer != 6) // 6 == player layer
			{
				return;
			}
			
			villagerController.OnMovementStopped -= EnableEffectsIfPossible;

			DisableEffects();
		}

		private void EnableEffectsIfPossible()
		{
			if (directionsReceiver.CanReceiveDirections)
			{
				EnableEffects();
			}
		}

		private void EnableEffects()
		{
			objectToEnable.SetActive(true);
			canGiveDirectionsSoundInstance.start();
		}

		private void DisableEffects()
		{
			objectToEnable.SetActive(false);
		}

		private void DisableEffects(Queue<AbstractMoveCommand> _)
		{
			DisableEffects();
		}

		private void OnDestroy()
		{
			villagerController.OnMovementStopped -= EnableEffectsIfPossible;
			canGiveDirectionsSoundInstance.release();
		}
	}
}