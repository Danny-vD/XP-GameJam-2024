using System;
using System.Collections.Generic;
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
		private Intersection previousNode;

		[SerializeField]
		private Intersection currentNode;

		[SerializeField]
		private InputActionReference interact;

		[SerializeField]
		private InputActionReference movement;

		[SerializeField]
		private GameObject exclamationMark;
		
		public bool CannotReceiveCommand => !isListening || agent.remainingDistance > 5 && !agent.isStopped;
		public bool CanReceiveCommand => !CannotReceiveCommand;

		private Queue<AbstractMoveCommand> commandsQueue;

		private Dictionary<Vector2, Func<AbstractMoveCommand>> commandByVector;

		private NavMeshAgent agent;
		private bool isListening;

		private Vector3 lastDirection = Vector3.zero;

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

			interact.action.performed += OnInteract;
			movement.action.performed += MovementOnPerformed;

			agent = GetComponent<NavMeshAgent>();
		}

		private void LateUpdate()
		{
			Vector3 currentDirection = agent.velocity.normalized;

			if (currentDirection.sqrMagnitude > Mathf.Epsilon)
			{
				lastDirection = currentDirection;
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer.Equals(6))
			{
				isListening = true;

				if (exclamationMark && CanReceiveCommand)
				{
					exclamationMark.SetActive(true);
				}
			}
			else
			{
				NextCommand();
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.layer.Equals(6))
			{
				isListening = false;
				
				if (exclamationMark)
				{
					exclamationMark.SetActive(false);
				}
			}
		}

		private void MovementOnPerformed(InputAction.CallbackContext obj)
		{
			if (CannotReceiveCommand)
			{
				return;
			}

			Vector2 vector = obj.ReadValue<Vector2>();

			if (!commandByVector.TryGetValue(vector, out Func<AbstractMoveCommand> command)) return;

			commandsQueue.Enqueue(command.Invoke());
		}

		private void OnInteract(InputAction.CallbackContext obj)
		{
			if (CannotReceiveCommand)
			{
				return;
			}

			if (commandsQueue.Count > 0)
			{
				if (exclamationMark)
				{
					exclamationMark.SetActive(false);
				}
			}
			
			NextCommand();
		}

		private void NextCommand()
		{
			if (commandsQueue.Count <= 0)
			{
				return;
			}

			Intersection nextNode = commandsQueue.Dequeue()?.CalculateNextNode(currentNode);

			if (nextNode is null)
			{
				return;
			}

			agent.SetDestination(nextNode.transform.position);
			previousNode = currentNode;
			currentNode  = nextNode;
			
			/*
			
			if (currentNode.Connections.Count <= 2)
			{
				Intersection nextNode = MoveForwardCommand.NewInstance().CalculateNextNode(currentNode);
				previousNode = currentNode;
				currentNode  = nextNode;
				agent.SetDestination(currentNode.transform.position);
			}
			else
			{
				if (commandsQueue.Count <= 0)
				{
					return;
				}

				Intersection nextNode = commandsQueue.Dequeue()?.CalculateNextNode(currentNode);

				if (nextNode is null)
				{
					return;
				}

				agent.SetDestination(nextNode.transform.position);
				previousNode = currentNode;
				currentNode  = nextNode;
			}
			*/
		}
	}
}