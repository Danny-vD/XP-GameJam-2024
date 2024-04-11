using System;
using System.Collections.Generic;
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
		public event Action<List<DirectionsReceiver>> OnInteractWithVillagers = delegate { };

		[SerializeField]
		private InputActionReference overworldInteract;

		private readonly List<IInteractable> interactables = new List<IInteractable>();
		private readonly List<DirectionsReceiver> villagers = new List<DirectionsReceiver>();

		private void OnEnable()
		{
			overworldInteract.action.performed += Interact;
		}

		private void OnDisable()
		{
			overworldInteract.action.performed -= Interact;
		}

		private void Interact(InputAction.CallbackContext obj)
		{
			if (villagers.Count > 0)
			{
				OnInteractWithVillagers.Invoke(villagers);

				return;
			}

			if (interactables.Count > 0)
			{
				foreach (IInteractable interactable in interactables)
				{
					interactable.Interact();
				}
			
				OnInteract.Invoke(interactables);

				return;
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IInteractable interactable))
			{
				interactables.Add(interactable);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent(out IInteractable interactable))
			{
				interactables.Remove(interactable);
			}
		}
	}
}