using System.Collections.Generic;
using MapMovement.Commands.Interface;
using UnityEngine;
using VDFramework;

namespace Gameplay.DirectionsSystem.NPCs
{
	[RequireComponent(typeof(DirectionsReceiver)), DisallowMultipleComponent]
	public class DirectionsHolder : BetterMonoBehaviour
	{
		public Queue<AbstractMoveCommand> KnownDirections { get; private set; } = new Queue<AbstractMoveCommand>();

		private DirectionsReceiver directionsReceiver;

		private void Awake()
		{
			directionsReceiver = GetComponent<DirectionsReceiver>();
		}

		private void OnEnable()
		{
			directionsReceiver.OnDirectionsReceived += SetDirections;
		}

		private void OnDisable()
		{
			directionsReceiver.OnDirectionsReceived -= SetDirections;
		}

		private void SetDirections(Queue<AbstractMoveCommand> directions)
		{
			KnownDirections = directions;
		}
	}
}