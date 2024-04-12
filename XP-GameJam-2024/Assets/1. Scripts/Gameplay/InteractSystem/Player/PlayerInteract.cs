using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.DirectionsSystem.NPCs;
using Gameplay.InteractSystem.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using VDFramework;

namespace Gameplay.InteractSystem.Player
{
	public class PlayerInteract : BetterMonoBehaviour
	{
		public event Action<List<IInteractable>> OnInteract = delegate { };
		public event Action<List<DirectionsReceiver>> OnInteractWithDirectionReceivers = delegate { };

		[SerializeField]
		private InputActionReference overworldInteractInput;

		private readonly List<IInteractable> interactables = new List<IInteractable>();
		private readonly List<DirectionsReceiver> directionsReceivers = new List<DirectionsReceiver>();

		private InteractablesChecker interactablesChecker;

		private void Awake()
		{
			interactablesChecker = GetComponentInChildren<InteractablesChecker>();
		}

		private void OnEnable()
		{
			interactablesChecker.OnDirectionsReceiverEnteredRadius += AddDirectionsReceiver;
			interactablesChecker.OnInteractableEnteredRadius       += AddInteractable;

			interactablesChecker.OnDirectionsReceiverLeftRadius += RemoveDirectionsReceiver;
			interactablesChecker.OnInteractableLeftRadius       += RemoveInteractable;

			overworldInteractInput.action.performed += Interact;
		}

		private void OnDisable()
		{
			interactablesChecker.OnDirectionsReceiverEnteredRadius -= AddDirectionsReceiver;
			interactablesChecker.OnInteractableEnteredRadius       -= AddInteractable;

			interactablesChecker.OnDirectionsReceiverLeftRadius -= RemoveDirectionsReceiver;
			interactablesChecker.OnInteractableLeftRadius       -= RemoveInteractable;

			overworldInteractInput.action.performed -= Interact;
		}

		private void Interact(InputAction.CallbackContext obj)
		{
			List<DirectionsReceiver> availableReceivers = directionsReceivers.Where(receiver => receiver.CanReceiveDirections).ToList();
			
			if (availableReceivers.Count > 0)
			{
				OnInteractWithDirectionReceivers.Invoke(availableReceivers);

				return;
			}

			List<IInteractable> availableInteractables = interactables.Where(interactable => interactable.CanInteract).ToList();
			
			if (availableInteractables.Count > 0)
			{
				foreach (IInteractable interactable in availableInteractables)
				{
					interactable.Interact();
				}

				OnInteract.Invoke(interactables);

				return;
			}
		}

		private void AddDirectionsReceiver(DirectionsReceiver directionsReceiver)
		{
			if (!directionsReceivers.Contains(directionsReceiver))
			{
				directionsReceivers.Add(directionsReceiver);
			}
		}

		private void RemoveDirectionsReceiver(DirectionsReceiver directionsReceiver)
		{
			directionsReceivers.Remove(directionsReceiver);
		}

		private void AddInteractable(IInteractable interactable)
		{
			if (!interactables.Contains(interactable))
			{
				interactables.Add(interactable);
			}
		}

		private void RemoveInteractable(IInteractable interactable)
		{
			interactables.Remove(interactable);
		}
	}
}