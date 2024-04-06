using System.Linq;
using MapMovement.Commands.Interface;
using MapMovement.Managers;
using MapMovement.Waypoints;
using UnityEngine;

namespace MapMovement.Commands
{
    public class MoveLeftCommand : AbstractMoveCommand
    {
        public override Intersection CalculateNextNode(Intersection currentNode, Intersection previousNode,
            Vector3 transformPosition)
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
                var tempAngle = Vector2.SignedAngle(currentNode.transform.position * new Vector2(transformPosition.x, transformPosition.z), currentNodeConnection.transform.position);
                Debug.Log(currentNodeConnection.ToString() + " " + tempAngle);
                
                if (tempAngle >= 30 && tempAngle >= a)
                {
                    b = currentNode.Connections.IndexOf(currentNodeConnection);
                };
            }

            return b == -1 ? null : currentNode.Connections[b];
        }

        
        public static AbstractMoveCommand NewInstance()
        {
            return new MoveLeftCommand();
        }
    }
}