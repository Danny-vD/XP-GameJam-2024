using Gameplay.DirectionsSystem.Waypoints;
using MapMovement.Commands.Interfaces;
using MapMovement.Enums;

namespace MapMovement.Commands
{
	public class MoveUpCommand : AbstractMoveCommand
	{
		public override bool TryGetNextNode(Intersection currentNode, out AbstractWayPoint nextPoint)
		{
			return currentNode.TryGetConnectingIntersection(Direction.Up, out nextPoint);
		}

		public static AbstractMoveCommand NewInstance()
		{
			return new MoveUpCommand();
		}
	}
}