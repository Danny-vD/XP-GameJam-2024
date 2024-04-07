using MapMovement.Waypoints;
using UnityEngine;

namespace MapMovement.Commands.Interface
{
    public abstract class AbstractMoveCommand
    {
        public abstract Intersection GetNextNode(Intersection currentNode);
        
        public abstract Intersection CalculateNextNode(Intersection currentNode, Intersection previousNode, Transform transform, Vector3 movementDirection);
    }
}