using Gameplay.DirectionsSystem.Waypoints;

namespace MapMovement.Commands.Interfaces
{
    public abstract class AbstractMoveCommand
    {
        public abstract bool TryGetNextNode(Intersection currentNode, out AbstractWayPoint nextPoint);
    }
}