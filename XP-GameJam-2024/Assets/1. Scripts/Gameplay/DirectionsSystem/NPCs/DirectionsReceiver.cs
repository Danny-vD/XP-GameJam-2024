using System;
using System.Collections.Generic;
using MapMovement.Commands.Interface;
using UnityEngine;
using VDFramework;

namespace Gameplay.DirectionsSystem.NPCs
{
	[RequireComponent(typeof(DirectionsHolder)), DisallowMultipleComponent]
	public class DirectionsReceiver : BetterMonoBehaviour
	{
		public event Action<Queue<AbstractMoveCommand>> OnDirectionsReceived = delegate { }; 
		public event Action OnDirectionsReceivedLate = delegate { }; 
		
		public bool CanReceiveDirections => directionsHolder.KnownDirections.Count == 0;

		private DirectionsHolder directionsHolder;

		private void Awake()
		{
			directionsHolder = GetComponent<DirectionsHolder>();
		}

		public void SetDirections(Queue<AbstractMoveCommand> directions)
		{
			OnDirectionsReceived.Invoke(directions);
			OnDirectionsReceivedLate.Invoke();
		}
	}
}