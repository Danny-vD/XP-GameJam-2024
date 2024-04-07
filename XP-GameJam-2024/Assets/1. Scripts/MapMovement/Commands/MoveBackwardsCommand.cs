using MapMovement.Commands.Interface;
using MapMovement.Waypoints;
using UnityEngine;

namespace MapMovement.Commands
{
	public class MoveBackwardsCommand : AbstractMoveCommand
	{
		public override Intersection CalculateNextNode(Intersection currentNode, Intersection previousNode, Transform transform, Vector3 movementDirection)
		{
			StartingNode = currentNode;
			return previousNode;
		}

		public static AbstractMoveCommand NewInstance()
		{
			return new MoveBackwardsCommand();
		}
	}
}