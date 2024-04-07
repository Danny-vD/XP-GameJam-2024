using System;
using System.Collections.Generic;
using System.Linq;
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
		public event Action OnMovementStart = delegate { };
		public event Action OnMovementCancelled = delegate { };
		public event Action OnEnterIdle = delegate { };

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

		public bool CannotReceiveCommand => !isListening || agent.remainingDistance > 5 && !agent.isStopped; //note Replace most of this by IsMoving?
		public bool CanReceiveCommand => !CannotReceiveCommand;

		public bool IsMoving { get; private set; }

		private Queue<AbstractMoveCommand> commandsQueue;

		private Dictionary<Vector2, Func<AbstractMoveCommand>> commandByVector;

		private NavMeshAgent agent;
		private bool isListening;


		//private Vector3 lastDirection = Vector3.zero;

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
			//Vector3 currentDirection = agent.velocity.normalized;
			//
			//if (currentDirection.sqrMagnitude > Mathf.Epsilon)
			//{
			//	lastDirection = currentDirection;
			//}

			if (IsMoving)
			{
				CheckIfReachedTarget();
			}
		}

		private void CheckIfReachedTarget()
		{
			float distance = agent.remainingDistance;
			distance -= agent.stoppingDistance;

			if (distance <= 0)
			{
				NextCommand();
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
			
			/*
			else
			{
				if (ReferenceEquals(other, lastIntersectionColliderHit))
				{
					return;
				}

				lastIntersectionColliderHit = other;

				Debug.Log($"reached {other.name}");
				NextCommand();
			}*/
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

			if (!commandByVector.TryGetValue(vector, out Func<AbstractMoveCommand> command))
			{
				return;
			}

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

				agent.isStopped = false;
				IsMoving        = true;
				OnMovementStart.Invoke();
				
				Debug.Log("\nStarting movement");
			}

			NextCommand();
		}

		private void NextCommand()
		{
			Intersection nextNode = null;

			if (currentNode.Connections.Count <= 2)
			{
				nextNode = currentNode.Connections.FirstOrDefault(connection => connection != previousNode);
				
				if (nextNode is null)
				{
					OnEnterIdle.Invoke();
				
					IsMoving        = false;
					agent.isStopped = true;
				
					commandsQueue.Clear();

					if (isListening && exclamationMark)
					{
						exclamationMark.SetActive(true);
					}

					return;
				}

				agent.SetDestination(currentNode.transform.position);

				previousNode = currentNode;
				currentNode  = nextNode;
				return;
			}

			if (commandsQueue.Count <= 0)
			{
				return;
			}

			nextNode = commandsQueue.Dequeue()?.GetNextNode(currentNode);

			if (nextNode is null)
			{
				OnEnterIdle.Invoke();
				
				IsMoving        = false;
				agent.isStopped = true;
				
				commandsQueue.Clear();

				if (isListening && exclamationMark)
				{
					exclamationMark.SetActive(true);
				}

				return;
			}

			agent.SetDestination(nextNode.transform.position);
			previousNode = currentNode;
			currentNode  = nextNode;

			/*
			Vector3 movementDirection = agent.velocity.normalized;

			if (movementDirection.sqrMagnitude < Mathf.Epsilon)
			{
				movementDirection = lastDirection; // First try the last used direction
			}

			if (movementDirection.sqrMagnitude < Mathf.Epsilon)
			{
				movementDirection = previousNode.transform.up; // If that is also 0,0,0 then use the rotation of the node
			}

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