using System;

namespace MapMovement
{
    public interface IActorMover
    {
        public event Action OnMovementStart;
        public event Action OnMovementCancelled;

        public event Action OnEnterIdle;

    }
}