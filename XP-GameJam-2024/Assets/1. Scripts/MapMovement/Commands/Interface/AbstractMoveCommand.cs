using System.Linq;
using MapMovement.Managers;
using MapMovement.Waypoints;
using UnityEngine;

namespace MapMovement.Commands.Interface
{
    public abstract class AbstractMoveCommand
    {
        protected Intersection StartingNode;

        public virtual Intersection CalculateNextNode(Intersection currentNode, Intersection previousNode,
            Vector3 transformPosition)
        {
            return IntersectionManager.Instance.IntersectionList.First();
        }
    }
}