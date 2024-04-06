using System.Linq;
using MapMovement.Commands.Interface;
using MapMovement.Managers;
using MapMovement.Waypoints;

namespace MapMovement.Commands
{
    public class MoveBackwardsCommand : AbstractMoveCommand
    {
        public override Intersection CalculateNextNode(Intersection currentNode)
        {
            StartingNode = currentNode;
            return StartingNode;
        }
        
        public static AbstractMoveCommand NewInstance()
        {
            return new MoveBackwardsCommand();
        }
    }
}