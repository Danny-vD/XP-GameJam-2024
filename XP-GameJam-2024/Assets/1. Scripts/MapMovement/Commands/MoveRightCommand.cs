using Gameplay.DirectionsSystem.Waypoints;
using MapMovement.Commands.Interface;
using MapMovement.Enums;

namespace MapMovement.Commands
{
	public class MoveRightCommand : AbstractMoveCommand
	{
		public override bool TryGetNextNode(Intersection currentNode, out AbstractWayPoint nextPoint)
		{
			return currentNode.TryGetConnectingIntersection(Direction.Right, out nextPoint);
		}

		public static AbstractMoveCommand NewInstance()
		{
			return new MoveRightCommand();
		}
	}
}