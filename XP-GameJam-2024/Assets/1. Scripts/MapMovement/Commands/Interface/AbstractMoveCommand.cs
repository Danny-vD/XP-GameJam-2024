using System.Linq;
using MapMovement.Managers;
using MapMovement.Waypoints;

namespace MapMovement.Commands.Interface
{
    public abstract class AbstractMoveCommand
    {
        protected Intersection StartingNode;

        public virtual Intersection CalculateNextNode(Intersection currentNode)
        {
            return IntersectionManager.Instance.IntersectionList.First();
        }
    }
}