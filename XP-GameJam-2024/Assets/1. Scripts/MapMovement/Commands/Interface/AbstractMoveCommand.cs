using Gameplay.DirectionsSystem.Waypoints;

namespace MapMovement.Commands.Interface
{
    public abstract class AbstractMoveCommand
    {
        public abstract bool TryGetNextNode(Intersection currentNode, out AbstractWayPoint nextPoint);
    }
}