using System;
using System.Collections.Generic;
using InteractSystem.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using VDFramework;

namespace InteractSystem.Player
{
	public class PlayerInteract : BetterMonoBehaviour
	{
		[SerializeField]
		private InputActionReference overworldInteract;

		private List<IInteractable> interactables = new List<IInteractable>();

		private void OnEnable()
		{
			overworldInteract.action.performed += TryInteract;
		}

		private void OnDisable()
		{
			overworldInteract.action.performed -= TryInteract;
		}

		private void TryInteract(InputAction.CallbackContext obj)
		{
			foreach (IInteractable interactable in interactables)
			{
				interactable.Interact();
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