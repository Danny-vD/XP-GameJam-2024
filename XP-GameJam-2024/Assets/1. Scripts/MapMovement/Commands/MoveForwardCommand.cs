using System.Linq;
using MapMovement.Commands.Interface;
using MapMovement.Waypoints;
using UnityEngine;
using VDFramework.Extensions;

namespace MapMovement.Commands
{
	public class MoveForwardCommand : AbstractMoveCommand
	{
		private const float forwardAngleThreshold = 45;
		
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
			
			//float unsignedAngle = Vector3.Angle(movementDirection, currentNode.Connections[0].transform.position - currentNode.transform.position);
			int bestCandidateIndex = -1;
			float lowestAngle = forwardAngleThreshold;

			Vector3 currentNodePosition = currentNode.transform.position;

			for (int i = 0; i < currentNode.Connections.Count; i++)
			{
				float angle = Vector3.Angle(movementDirection, currentNode.Connections[i].transform.position - currentNodePosition);

				if (angle <= lowestAngle)
				{
					bestCandidateIndex = i;
					lowestAngle        = angle;
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