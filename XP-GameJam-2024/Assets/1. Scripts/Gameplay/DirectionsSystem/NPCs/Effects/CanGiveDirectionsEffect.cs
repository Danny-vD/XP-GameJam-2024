using FMOD.Studio;
using FMODUtilityPackage.Core;
using FMODUtilityPackage.Enums;
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
		
		private void Awake()
		{
			directionsReceiver = GetComponent<DirectionsReceiver>();
			canGiveDirectionsSoundInstance = AudioPlayer.GetEventInstance(canGiveDirectionsSound);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer != 6) // 6 == player layer
			{
				return;
			}
			
			if (directionsReceiver.CanReceiveDirections)
			{
				objectToEnable.SetActive(true);
				canGiveDirectionsSoundInstance.start();
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.layer != 6) // 6 == player layer
			{
				return;
			}
			
			objectToEnable.SetActive(false);
		}

		private void OnDestroy()
		{
			canGiveDirectionsSoundInstance.release();
		}
	}
}