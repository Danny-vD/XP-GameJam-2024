using System;
using System.Collections.Generic;
using System.Linq;
using Input;
using MapMovement.Commands;
using MapMovement.Commands.Interface;
using MapMovement.Waypoints;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using VDFramework;

namespace MapMovement.NPCs
{
	public class VillagerController : BetterMonoBehaviour, IActorMover
	{
		public event Action OnMovementStart;
		public event Action OnMovementCancelled;
		public event Action OnEnterIdle;

		private bool listeningMode;

		[SerializeField]
		private Intersection currentNode;

		[SerializeField] private InputActionReference interact;
		[SerializeField] private InputActionReference movement;

		private Queue<AbstractMoveCommand> commandsQueue;

		private Dictionary<Vector2, Func<AbstractMoveCommand>> commandByVector;

		private NavMeshAgent agent;

		private void Awake()
		{
			commandByVector = new Dictionary<Vector2, Func<AbstractMoveCommand>>
			{
				{ Vector2.down, MoveBackwardsCommand.NewInstance },
				{ Vector2.up, MoveForwardCommand.NewInstance },
				{ Vector2.left, MoveLeftCommand.NewInstance },
				{ Vector2.right, MoveRightCommand.NewInstance },
			};

			interact.action.performed += OnInteract;
			movement.action.performed += MovementOnPerformed;
			
			agent = GetComponent<NavMeshAgent>();
		}
		
		private void OnTriggerEnter(Collider other)
		{
			if (!listeningMode) NextCommand();
		}

		private void MovementOnPerformed(InputAction.CallbackContext obj)
		{
			if (!listeningMode) return;

			Vector2 vector = obj.ReadValue<Vector2>();

			if (commandByVector.TryGetValue(vector, out Func<AbstractMoveCommand> command))
			{
				commandsQueue.Enqueue(command.Invoke());
			}
		}

		private void OnInteract(InputAction.CallbackContext obj)
		{
			if (!listeningMode)
			{
				listeningMode = true;
			}
			else
			{
				listeningMode = false;
				NextCommand();
			}
		}

		private void NextCommand()
		{
			if (currentNode.Connections.Count <= 2)
			{
				agent.SetDestination(MoveForwardCommand.NewInstance().CalculateNextNode(currentNode).transform.position);
			}
			else
			{
				var nextNode = commandsQueue.Dequeue()?.CalculateNextNode(currentNode);

				if (nextNode is not null)
				{
					agent.SetDestination(nextNode.transform.position);
				}
			}
		}
	}
}