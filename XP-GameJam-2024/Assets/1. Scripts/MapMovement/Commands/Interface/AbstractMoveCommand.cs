using System.Linq;
using MapMovement.Managers;
using MapMovement.Waypoints;
using UnityEngine;

namespace MapMovement.Commands.Interface
{
    public abstract class AbstractMoveCommand
    {
        protected Intersection StartingNode;

        public abstract Intersection CalculateNextNode(Intersection currentNode, Intersection previousNode, Transform transform);
    }
}