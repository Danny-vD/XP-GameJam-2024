using System;
using System.Collections.Generic;
using MapMovement.Commands.Interface;
using MapMovement.NPCs;
using UnityEngine;
using VDFramework;

namespace Gameplay.DirectionsSystem.NPCs
{
	[RequireComponent(typeof(DirectionsHolder)), DisallowMultipleComponent]
	public class DirectionsReceiver : BetterMonoBehaviour
	{
		public event Action<Queue<AbstractMoveCommand>> OnDirectionsReceived = delegate { }; 
		public event Action OnDirectionsReceivedLate = delegate { }; 
		
		public bool CanReceiveDirections => directionsHolder.KnownDirections.Count == 0 && !navMeshAgentManager.IsMoving;

		private DirectionsHolder directionsHolder;
		private NavMeshAgentManager navMeshAgentManager;

		private void Awake()
		{
			directionsHolder    = GetComponent<DirectionsHolder>();
			navMeshAgentManager = GetComponent<NavMeshAgentManager>();
		}

		public void SetDirections(Queue<AbstractMoveCommand> directions)
		{
			if (directions == null || directions.Count == 0)
			{
				return;
			}
			
			OnDirectionsReceived.Invoke(directions);
			OnDirectionsReceivedLate.Invoke();
		}
	}
}