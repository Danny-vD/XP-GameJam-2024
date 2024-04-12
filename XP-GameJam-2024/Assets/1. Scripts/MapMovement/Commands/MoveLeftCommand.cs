using System.Linq;
using MapMovement.Commands.Interface;
using MapMovement.Enums;
using MapMovement.Waypoints;
using UnityEngine;
using VDFramework.Extensions;

namespace MapMovement.Commands
{
	public class MoveLeftCommand : AbstractMoveCommand
	{
		public override Intersection GetNextNode(Intersection currentNode) => currentNode.GetConnectingIntersection(Direction.Left);

		public override Intersection CalculateNextNode(Intersection currentNode, Intersection previousNode, Transform transform, Vector3 movementDirection)
		{
			if (currentNode.Connections.CountIsZeroOrOne())
			{
				return currentNode.Connections.FirstOrDefault();
			}

			// The found left might actually be the forward direction, so we also calculate the forward direction to compare afterwards
			int bestCandidateIndex = -1;
			int forwardDirectionIndex = -1;

			float lowestSignedAngle = 0;
			float lowestUnsignedAngle = MoveUpCommand.FORWARD_ANGLE_THRESHOLD;

			Vector3 currentNodePosition = currentNode.transform.position;

			for (int i = 0; i < currentNode.Connections.Count; i++)
			{
				Intersection connection = currentNode.Connections[i];

				if (connection == previousNode) // Skip the node we came from
				{
					continue;
				}
				
				float signedAngle = Vector3.SignedAngle(movementDirection, connection.transform.position - currentNodePosition, Vector3.up);

				// Find the lowest signed angle
				if (signedAngle <= lowestSignedAngle)
				{
					bestCandidateIndex = i;
					lowestSignedAngle  = signedAngle;
				}

				// Keep track of the forward direction
				float unsignedAngle = Mathf.Abs(signedAngle);

				if (unsignedAngle <= lowestUnsignedAngle)
				{
					lowestUnsignedAngle   = unsignedAngle;
					forwardDirectionIndex = i;
				}
			}

			// Check that the most likely direction for left is not the forward direction
			bool notForwardDirection = bestCandidateIndex != forwardDirectionIndex;

			return bestCandidateIndex == -1 && notForwardDirection ? null : currentNode.Connections[bestCandidateIndex];
		}


		public static AbstractMoveCommand NewInstance()
		{
			return new MoveLeftCommand();
		}
	}
}