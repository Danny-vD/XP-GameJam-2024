using System;
using Gameplay.DirectionsSystem.NPCs;
using Gameplay.InteractSystem.Interfaces;
using UnityEngine;
using VDFramework;

namespace Gameplay.InteractSystem.Player
{
	public class InteractablesChecker : BetterMonoBehaviour
	{
		public event Action<DirectionsReceiver> OnDirectionsReceiverEnteredRadius = delegate { };
		public event Action<DirectionsReceiver> OnDirectionsReceiverLeftRadius = delegate { };

		public event Action<IInteractable> OnInteractableEnteredRadius = delegate { };
		public event Action<IInteractable> OnInteractableLeftRadius = delegate { };

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out DirectionsReceiver directionsReceiver))
			{
				OnDirectionsReceiverEnteredRadius.Invoke(directionsReceiver);
			}
			
			if (other.TryGetComponent(out IInteractable interactable))
			{
				OnInteractableEnteredRadius.Invoke(interactable);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent(out DirectionsReceiver directionsReceiver))
			{
				OnDirectionsReceiverLeftRadius.Invoke(directionsReceiver);
			}
			
			if (other.TryGetComponent(out IInteractable interactable))
			{
				OnInteractableLeftRadius.Invoke(interactable);
			}
		}
	}
}