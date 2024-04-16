using System.Linq;
using UnityEngine;

namespace Gameplay.DirectionsSystem.Waypoints
{
    public class RoutePoint : AbstractWayPoint
    {
        [SerializeField]
        private AbstractWayPoint connection1;
        
        [SerializeField]
        private AbstractWayPoint connection2;

        private void Start()
        {
            connections    = new AbstractWayPoint[2];
            connections[0] = connection1;
            connections[1] = connection2;
        }

        public AbstractWayPoint GetNextWayPoint(AbstractWayPoint previous)
        {
            AbstractWayPoint nextWayPoint = connections.FirstOrDefault(waypoint => waypoint != previous);

            if (ReferenceEquals(nextWayPoint, null))
            {
                return connections[0]; // Take the first connection if there is only one
            }

            return nextWayPoint;
        }
    }
}