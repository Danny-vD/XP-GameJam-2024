using System.Linq;
using MapMovement.Commands.Interface;
using MapMovement.Managers;
using MapMovement.Waypoints;
using UnityEngine;

namespace MapMovement.Commands
{
	public class MoveForwardCommand : AbstractMoveCommand
	{
		public override Intersection CalculateNextNode(Intersection currentNode, Intersection previousNode, Transform transform)
		{
			float a = Vector3.Angle(currentNode.transform.forward, currentNode.Connections[0].transform.up);
			int b = -1;

			if (currentNode.Connections.Count == 2)
			{
				foreach (Intersection currentNodeConnection in currentNode.Connections)
				{
					Debug.Log(currentNodeConnection + "  " + previousNode);

					if (currentNodeConnection != previousNode)
					{
						return currentNodeConnection;
					}
				}
			}

			foreach (Intersection currentNodeConnection in currentNode.Connections)
			{
				float tempAngle = Vector2.SignedAngle(currentNode.transform.position * new Vector2(transform.position.x, transform.position.z),
					currentNodeConnection.transform.position * new Vector2(currentNodeConnection.transform.forward.x, currentNodeConnection.transform.forward.z));

				if (tempAngle <= 30 && tempAngle <= a)
				{
					b = currentNode.Connections.IndexOf(currentNodeConnection);
				}

				;
			}

			return b == -1 ? null : currentNode.Connections[b];
		}


		public static AbstractMoveCommand NewInstance()
		{
			return new MoveForwardCommand();
		}
	}
}