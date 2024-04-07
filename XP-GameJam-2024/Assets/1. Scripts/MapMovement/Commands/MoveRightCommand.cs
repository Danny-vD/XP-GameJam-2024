using System.Linq;
using MapMovement.Commands.Interface;
using MapMovement.Managers;
using MapMovement.Waypoints;
using UnityEngine;

namespace MapMovement.Commands
{
    public class MoveRightCommand : AbstractMoveCommand
    {
        public override Intersection CalculateNextNode(Intersection currentNode, Intersection previousNode, Transform transform)
        {
            StartingNode = currentNode;

            var a = Vector3.Angle(currentNode.transform.forward, currentNode.Connections[0].transform.forward);
            var b = -1;

            if (currentNode.Connections.Count <= 2)
            {
                return IntersectionManager.Instance.IntersectionList.First();
            }
            
            foreach (var currentNodeConnection in currentNode.Connections)
            {
                var tempAngle = Vector2.SignedAngle(currentNode.transform.position * new Vector2(transform.position.x, transform.position.z), currentNodeConnection.transform.position * new Vector2(currentNodeConnection.transform.forward.x, currentNodeConnection.transform.forward.z));
                
                Debug.Log(currentNodeConnection.ToString() + " " + tempAngle);
                
                if (tempAngle <= 30 && tempAngle <= a)
                {
                    b = currentNode.Connections.IndexOf(currentNodeConnection);
                };
            }

            return b == -1 ? null : currentNode.Connections[b];
        }
        public static AbstractMoveCommand NewInstance()
        {
            return new MoveRightCommand();
        }
    }
}