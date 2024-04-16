using System;
using UnityEngine;
using UnityEngine.AI;
using VDFramework;

namespace MapMovement.NPCs
{
	[DisallowMultipleComponent, RequireComponent(typeof(NavMeshAgent))]
	public class NavMeshAgentManager : BetterMonoBehaviour
	{
		public event Action OnDestinationSet = delegate { };
		public event Action OnDestinationReached = delegate { };

		public event Action OnMovementStarted = delegate { };
		public event Action OnMovementStopped = delegate { };

		public bool IsMoving => agent.hasPath && !agent.isStopped;
		public float DistanceToTarget => agent.remainingDistance - agent.stoppingDistance;
		public float DistanceToTargetNormalized => DistanceToTarget / (distanceAtStart - agent.stoppingDistance);

		private NavMeshAgent agent;
		private float distanceAtStart = 0;

		private void Awake()
		{
			agent = GetComponent<NavMeshAgent>();
		}

		private void LateUpdate()
		{
			if (IsMoving && DistanceToTarget <= 0)
			{
				agent.isStopped = true;

				OnDestinationReached.Invoke();
				OnMovementStopped.Invoke();
			}
		}

		public bool SetDestination(Vector3 destination)
		{
			if (agent.SetDestination(destination))
			{
				OnDestinationSet.Invoke();

				if (!agent.isStopped)
				{
					OnMovementStarted.Invoke();
				}

				distanceAtStart = (destination - agent.nextPosition).magnitude;
				return true;
			}

			return false;
		}

		public void Stop()
		{
			agent.isStopped = true;
			OnMovementStopped.Invoke();
		}

		public void Start()
		{
			agent.isStopped = false;

			if (agent.hasPath)
			{
				OnMovementStarted.Invoke();
			}
		}
	}
}