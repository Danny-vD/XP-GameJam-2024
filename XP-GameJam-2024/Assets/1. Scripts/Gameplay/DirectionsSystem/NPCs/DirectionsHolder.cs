using System.Collections.Generic;
using MapMovement.Commands.Interface;
using UnityEngine;
using VDFramework;

namespace Gameplay.DirectionsSystem.NPCs
{
	[DisallowMultipleComponent]
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

		public bool TryGetNextDirection(out AbstractMoveCommand moveCommand)
		{
			return KnownDirections.TryDequeue(out moveCommand);
		}

		public void ClearAllDirections()
		{
			KnownDirections.Clear();
		}

		private void SetDirections(Queue<AbstractMoveCommand> directions)
		{
			KnownDirections = new Queue<AbstractMoveCommand>(directions);
		}
	}
}