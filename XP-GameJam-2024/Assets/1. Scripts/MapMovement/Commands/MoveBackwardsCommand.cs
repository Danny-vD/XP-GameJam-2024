using MapMovement.Commands.Interface;
using MapMovement.Enums;
using MapMovement.Waypoints;
using UnityEngine;

namespace MapMovement.Commands
{
	public class MoveBackwardsCommand : AbstractMoveCommand
	{
		public override Intersection GetNextNode(Intersection currentNode) => currentNode.GetConnectingIntersection(Direction.Down);

		public override Intersection CalculateNextNode(Intersection currentNode, Intersection previousNode, Transform transform, Vector3 movementDirection)
		{
			return previousNode;
		}

		public static AbstractMoveCommand NewInstance()
		{
			return new MoveBackwardsCommand();
		}
	}
}