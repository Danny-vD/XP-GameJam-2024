using System.Linq;
using MapMovement.Commands.Interface;
using MapMovement.Enums;
using MapMovement.Waypoints;
using UnityEngine;
using VDFramework.Extensions;

namespace MapMovement.Commands
{
	public class MoveForwardCommand : AbstractMoveCommand
	{
		public const float FORWARD_ANGLE_THRESHOLD = 45;

		public override Intersection GetNextNode(Intersection currentNode) => currentNode.GetConnectingIntersection(Direction.Up);

		public override Intersection CalculateNextNode(Intersection currentNode, Intersection previousNode, Transform transform, Vector3 movementDirection)
		{
			if (currentNode.Connections.Count == 2)
			{
				foreach (Intersection currentNodeConnection in currentNode.Connections)
				{
					if (currentNodeConnection != previousNode)
					{
						return currentNodeConnection;
					}
				}
			}

			if (currentNode.Connections.CountIsZeroOrOne())
			{
				return currentNode.Connections.FirstOrDefault();
			}
			
			int bestCandidateIndex = -1;
			float lowestAngle = FORWARD_ANGLE_THRESHOLD;

			Vector3 currentNodePosition = currentNode.transform.position;

			for (int i = 0; i < currentNode.Connections.Count; i++)
			{
				Intersection connection = currentNode.Connections[i];

				if (connection == previousNode) // Skip the node we came from
				{
					continue;
				}
				
				float unsignedAngle = Vector3.Angle(movementDirection, connection.transform.position - currentNodePosition);

				// Find the lowest unsigned angle
				if (unsignedAngle <= lowestAngle)
				{
					bestCandidateIndex = i;
					lowestAngle        = unsignedAngle;
				}
			}

			return bestCandidateIndex == -1 ? null : currentNode.Connections[bestCandidateIndex];
		}


		public static AbstractMoveCommand NewInstance()
		{
			return new MoveForwardCommand();
		}
	}
}