using System;
using Gameplay.DirectionsSystem.NPCs;
using Gameplay.DirectionsSystem.Waypoints;
using MapMovement.Commands.Interfaces;
using MapMovement.Interfaces;
using UnityEngine;
using VDFramework;

namespace MapMovement.NPCs
{
	[DisallowMultipleComponent]
	public class VillagerController : BetterMonoBehaviour, IActorMover
	{
		public event Action OnMovementStart = delegate { };
		public event Action OnMovementStopped = delegate { };

		private DirectionsReceiver directionsReceiver;
		private DirectionsHolder directionsHolder;

		private AbstractWayPoint currentWaypoint;
		private AbstractWayPoint previousWaypoint;

		private NavMeshAgentManager navMeshAgentManager;

		private void Awake()
		{
			directionsReceiver = GetComponent<DirectionsReceiver>();
			directionsHolder   = GetComponent<DirectionsHolder>();

			navMeshAgentManager = GetComponent<NavMeshAgentManager>();
		}

		private void Start()
		{
			currentWaypoint = AbstractWayPoint.GetClosestWayPoint(transform.position);
		}

		private void OnEnable()
		{
			directionsReceiver.OnDirectionsReceivedLate += StartMoving;
			navMeshAgentManager.OnDestinationReached    += NavigateToNextWaypoint;
		}

		private void OnDisable()
		{
			directionsReceiver.OnDirectionsReceivedLate -= StartMoving;
			navMeshAgentManager.OnDestinationReached    -= NavigateToNextWaypoint;
		}

		private void StartMoving()
		{
			while (currentWaypoint is RoutePoint)
			{
				currentWaypoint = currentWaypoint.GetFirstConnection();
			}

			OnMovementStart.Invoke();
			NavigateToNextWaypoint();
		}

		private void NavigateToNextWaypoint()
		{
			if (TryGetNextWaypoint(out AbstractWayPoint nextPoint))
			{
				if (!navMeshAgentManager.SetDestination(nextPoint.Position))
				{
					Debug.LogError($"Could not set navmesh destination from {currentWaypoint.name} to {nextPoint.name}");
				}

				previousWaypoint = currentWaypoint;
				currentWaypoint  = nextPoint;
				
				navMeshAgentManager.Start();
				return;
			}

			directionsHolder.ClearAllDirections();
			navMeshAgentManager.Stop();
			OnMovementStopped.Invoke();
		}

		private bool TryGetNextWaypoint(out AbstractWayPoint nextPoint)
		{
			nextPoint = null;

			if (currentWaypoint is Intersection currentIntersection)
			{
				if (directionsHolder.TryGetNextDirection(out AbstractMoveCommand moveCommand))
				{
					if (moveCommand.TryGetNextNode(currentIntersection, out nextPoint))
					{
						return true;
					}
				}
			}
			else if (currentWaypoint is RoutePoint wayPoint)
			{
				nextPoint = wayPoint.GetNextWayPoint(previousWaypoint); //TODO: keep track of previous
				return true;
			}

			return false;
		}
	}
}