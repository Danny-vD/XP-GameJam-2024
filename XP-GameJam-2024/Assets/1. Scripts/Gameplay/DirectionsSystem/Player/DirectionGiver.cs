using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.DirectionsSystem.NPCs;
using Gameplay.DirectionsSystem.Utility;
using Gameplay.InteractSystem.Player;
using MapMovement.Commands.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using VDFramework;

namespace Gameplay.DirectionsSystem.Player
{
	public class DirectionGiver : BetterMonoBehaviour
	{
		public event Action<AbstractMoveCommand> OnDirectionAdded = delegate { };
		public event Action<AbstractMoveCommand> OnDirectionRemoved = delegate { };

		[SerializeField]
		private InputActionReference specialMovementInput;

		[SerializeField]
		private InputActionReference specialInteractInput;

		[SerializeField]
		private InputActionReference removeLastDirectionInput;

		private List<DirectionsReceiver> directionsReceivers;

		private PlayerInteract playerInteract;

		private readonly Queue<AbstractMoveCommand> directions = new Queue<AbstractMoveCommand>();

		private void Awake()
		{
			playerInteract = GetComponent<PlayerInteract>();
		}

		private void OnEnable()
		{
			playerInteract.OnInteractWithDirectionReceivers += SetDirectionReceivers;

			specialMovementInput.action.performed     += AddDirection;
			removeLastDirectionInput.action.performed += RemoveLastDirection;
			specialInteractInput.action.performed     += SendDirections;
		}

		private void SetDirectionReceivers(List<DirectionsReceiver> directionsReceiversParam)
		{
			directions.Clear();
			directionsReceivers = directionsReceiversParam;
		}

		private void SendDirections(InputAction.CallbackContext callbackContext)
		{
			if (directions.Count == 0)
			{
				return;
			}

			foreach (DirectionsReceiver directionsReceiver in directionsReceivers.Where(directionsReceiver => directionsReceiver.CanReceiveDirections))
			{
				directionsReceiver.SetDirections(directions);
			}
		}

		private void AddDirection(InputAction.CallbackContext callbackContext)
		{
			Vector2 direction = callbackContext.ReadValue<Vector2>();

			if (Vector2ToMovementCommand.TryGetCommand(direction, out AbstractMoveCommand moveCommand))
			{
				directions.Enqueue(moveCommand);
				OnDirectionAdded.Invoke(moveCommand);
			}
		}

		private void RemoveLastDirection(InputAction.CallbackContext callbackContext)
		{
			if (directions.TryDequeue(out AbstractMoveCommand moveCommand))
			{
				OnDirectionRemoved.Invoke(moveCommand);
			}
		}
	}
}