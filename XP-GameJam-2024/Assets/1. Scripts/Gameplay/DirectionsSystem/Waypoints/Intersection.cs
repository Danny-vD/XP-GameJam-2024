using System.Collections.Generic;
using MapMovement.Enums;
using SerializableDictionaryPackage.SerializableDictionary;
using UnityEngine;

namespace Gameplay.DirectionsSystem.Waypoints
{
	public class Intersection : AbstractWayPoint
	{
		[SerializeField]
		private SerializableDictionary<Direction, AbstractWayPoint> directionalConnections;

		private void Start()
		{
			connections = new AbstractWayPoint[directionalConnections.Count];

			int index = 0;
			
			foreach (KeyValuePair<Direction, AbstractWayPoint> pair in directionalConnections)
			{
				connections[index] = pair.Value;

				++index;
			}
		}

		public bool TryGetConnectingIntersection(Direction direction, out AbstractWayPoint wayPoint)
		{
			bool success = directionalConnections.TryGetValue(direction, out wayPoint);

			return success && !ReferenceEquals(wayPoint, null);
		}
	}
}