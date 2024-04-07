using System.Linq;
using MapMovement.Commands.Interface;
using MapMovement.Managers;
using MapMovement.Waypoints;
using UnityEngine;
using VDFramework.Extensions;

namespace MapMovement.Commands
{
	public class MoveLeftCommand : AbstractMoveCommand
	{
		public override Intersection CalculateNextNode(Intersection currentNode, Intersection previousNode, Transform transform, Vector3 movementDirection)
		{
			StartingNode = currentNode;

			if (currentNode.Connections.CountIsZeroOrOne())
			{
				return currentNode.Connections.FirstOrDefault();
			}

			// The found left might actually be the forward direction, so we have to keep track of the forward direction until we're sure that we are the left (by being below the forward threshold)
			int bestCandidateIndex = -1;
			int forwardDirectionIndex = -1;

			float lowestSignedAngle = 0;
			float lowestUnsignedAngle = MoveForwardCommand.FORWARD_ANGLE_THRESHOLD;

			Vector3 currentNodePosition = currentNode.transform.position;

			for (int i = 0; i < currentNode.Connections.Count; i++)
			{
				float signedAngle = Vector3.SignedAngle(movementDirection, currentNode.Connections[i].transform.position - currentNodePosition, Vector3.up);

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