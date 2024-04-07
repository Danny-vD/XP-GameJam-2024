using System.Linq;
using MapMovement.Commands.Interface;
using MapMovement.Managers;
using MapMovement.Waypoints;
using UnityEngine;
using VDFramework.Extensions;

namespace MapMovement.Commands
{
	public class MoveRightCommand : AbstractMoveCommand
	{
		public override Intersection CalculateNextNode(Intersection currentNode, Intersection previousNode, Transform transform, Vector3 movementDirection)
		{
			StartingNode = currentNode;
			
			if (currentNode.Connections.CountIsZeroOrOne())
			{
				return currentNode.Connections.FirstOrDefault();
			}

			// The found right might actually be the forward direction, so we also calculate the forward direction to compare afterwards
			int bestCandidateIndex = -1;
			int forwardDirectionIndex = -1;

			float highestSignedAngle = 0;
			float lowestUnsignedAngle = MoveForwardCommand.FORWARD_ANGLE_THRESHOLD;

			Vector3 currentNodePosition = currentNode.transform.position;

			for (int i = 0; i < currentNode.Connections.Count; i++)
			{
				float signedAngle = Vector3.SignedAngle(movementDirection, currentNode.Connections[i].transform.position - currentNodePosition, Vector3.up);

				// Find the highest signed angle
				if (signedAngle >= highestSignedAngle)
				{
					bestCandidateIndex = i;
					highestSignedAngle  = signedAngle;
				}

				// Keep track of the forward direction
				float unsignedAngle = Mathf.Abs(signedAngle);

				if (unsignedAngle <= lowestUnsignedAngle)
				{
					lowestUnsignedAngle   = unsignedAngle;
					forwardDirectionIndex = i;
				}
			}

			// Check that the most likely direction for right is not the forward direction
			bool notForwardDirection = bestCandidateIndex != forwardDirectionIndex;

			return bestCandidateIndex == -1 && notForwardDirection ? null : currentNode.Connections[bestCandidateIndex];
			
			
			
			
			
			////////////////////////////////////////////////////////////////////////////
			float a = Vector3.Angle(currentNode.transform.forward, currentNode.Connections[0].transform.forward);
			int b = -1;

			if (currentNode.Connections.Count <= 2)
			{
				return IntersectionManager.Instance.IntersectionList.First();
			}

			foreach (Intersection currentNodeConnection in currentNode.Connections)
			{
				float tempAngle = Vector2.SignedAngle(currentNode.transform.position * new Vector2(transform.position.x, transform.position.z),
					currentNodeConnection.transform.position * new Vector2(currentNodeConnection.transform.forward.x, currentNodeConnection.transform.forward.z));

				Debug.Log(currentNodeConnection.ToString() + " " + tempAngle);

				if (tempAngle <= 30 && tempAngle <= a)
				{
					b = currentNode.Connections.IndexOf(currentNodeConnection);
				}
			}

			return b == -1 ? null : currentNode.Connections[b];
		}

		public static AbstractMoveCommand NewInstance()
		{
			return new MoveRightCommand();
		}
	}
}