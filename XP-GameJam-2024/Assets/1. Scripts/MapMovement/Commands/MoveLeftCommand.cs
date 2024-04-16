using Gameplay.DirectionsSystem.Waypoints;
using MapMovement.Commands.Interfaces;
using MapMovement.Enums;

namespace MapMovement.Commands
{
	public class MoveLeftCommand : AbstractMoveCommand
	{
		public override bool TryGetNextNode(Intersection currentNode, out AbstractWayPoint nextPoint)
		{
			return currentNode.TryGetConnectingIntersection(Direction.Left, out nextPoint);
		}

		public static AbstractMoveCommand NewInstance()
		{
			return new MoveLeftCommand();
		}
	}
}