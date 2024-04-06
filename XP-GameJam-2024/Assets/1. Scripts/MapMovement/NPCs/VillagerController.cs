using System;
using System.Collections.Generic;
using MapMovement.Commands;
using MapMovement.Commands.Interface;
using MapMovement.Waypoints;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using VDFramework;

namespace MapMovement.NPCs
{
	public class VillagerController : BetterMonoBehaviour, IActorMover
	{
		public event Action OnMovementStart;
		public event Action OnMovementCancelled;
		public event Action OnEnterIdle;

		[SerializeField]
		private Intersection previousNode;

		[SerializeField]
		private Intersection currentNode;

		[SerializeField]
		private InputActionReference specialInteract;

		[SerializeField]
		private InputActionReference specialMovement;

		private Queue<AbstractMoveCommand> commandsQueue;

		private Dictionary<Vector2, Func<AbstractMoveCommand>> commandByVector;

		private NavMeshAgent agent;
		
		private bool listeningMode;

		private void Awake()
		{
			commandsQueue = new Queue<AbstractMoveCommand>();

			commandByVector = new Dictionary<Vector2, Func<AbstractMoveCommand>>
			{
				{ Vector2.down, MoveBackwardsCommand.NewInstance },
				{ Vector2.up, MoveForwardCommand.NewInstance },
				{ Vector2.left, MoveLeftCommand.NewInstance },
				{ Vector2.right, MoveRightCommand.NewInstance },
			};

			specialInteract.action.performed += OnInteract;
			specialMovement.action.performed += MovementOnPerformed;

			agent = GetComponent<NavMeshAgent>();
		}

		private void OnTriggerEnter(Collider other)
		{
			NextCommand();
		}

		private void MovementOnPerformed(InputAction.CallbackContext obj)
		{
			Vector2 vector = obj.ReadValue<Vector2>();

			if (!commandByVector.TryGetValue(vector, out Func<AbstractMoveCommand> command)) return;

			Debug.Log(vector);
			commandsQueue.Enqueue(command.Invoke());
		}

		private void OnInteract(InputAction.CallbackContext obj)
		{
			NextCommand();
		}

		private void NextCommand()
		{
			if (currentNode.Connections.Count <= 2)
			{
				Intersection nextNode = MoveForwardCommand.NewInstance().CalculateNextNode(currentNode, previousNode, transform);
				previousNode = currentNode;
				currentNode  = nextNode;
				agent.SetDestination(currentNode.transform.position);
			}
			else
			{
				if (commandsQueue.Count <= 0) return;

				Intersection nextNode = commandsQueue.Dequeue()?.CalculateNextNode(currentNode, previousNode, transform);

				if (nextNode is null) return;

				agent.SetDestination(nextNode.transform.position);
				previousNode = currentNode;
				currentNode  = nextNode;
			}
		}

		private void OnDestroy()
		{
			specialInteract.action.performed -= OnInteract;
			specialMovement.action.performed -= MovementOnPerformed;
		}
	}
}