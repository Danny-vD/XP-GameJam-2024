using System;
using Gameplay.DirectionsSystem.NPCs;
using VDFramework;

namespace MapMovement.NPCs
{
	public class VillagerController : BetterMonoBehaviour, IActorMover
	{
		public event Action OnMovementStart = delegate { };
		public event Action OnEnterIdle = delegate { };

		private DirectionsReceiver directionsReceiver;
		private DirectionsHolder directionsHolder;

		private void Awake()
		{
			directionsReceiver = GetComponent<DirectionsReceiver>();
		}

		private void OnEnable()
		{
			directionsReceiver.OnDirectionsReceivedLate += StartMoving;
		}

		private void OnDisable()
		{
			directionsReceiver.OnDirectionsReceivedLate -= StartMoving;
		}

		private void StartMoving()
		{
			throw new NotImplementedException();
		}
	}
}