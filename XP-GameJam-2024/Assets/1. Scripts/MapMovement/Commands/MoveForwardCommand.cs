using System.Linq;
using MapMovement.Commands.Interface;
using MapMovement.Managers;
using MapMovement.Waypoints;
using UnityEngine;

namespace MapMovement.Commands
{
    public class MoveForwardCommand : AbstractMoveCommand
    {
        
        public override Intersection CalculateNextNode(Intersection currentNode)
        {

            var a = Vector3.Angle(currentNode.transform.forward, currentNode.Connections[0].transform.up);
            var b = -1;
            
            foreach (var currentNodeConnection in currentNode.Connections)
            {
                var tempAngle = Vector3.Angle(currentNode.transform.forward, currentNodeConnection.transform.up);
                if (tempAngle <= 30 && tempAngle <= a)
                {
                    b = currentNode.Connections.IndexOf(currentNodeConnection);
                };
            }
            
            return b == -1 ? null : currentNode.Connections[b];
        }
        
        
        public static AbstractMoveCommand NewInstance()
        {
            return new MoveForwardCommand();
        }
    }
}