using System;

namespace MapMovement.Interfaces
{
	public interface IActorMover
	{
		public event Action OnMovementStart;

		public event Action OnMovementStopped;
	}
}