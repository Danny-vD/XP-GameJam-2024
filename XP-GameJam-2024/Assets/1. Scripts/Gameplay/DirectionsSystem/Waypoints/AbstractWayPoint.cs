using System.Collections.Generic;
using UnityEngine;
using VDFramework;

namespace Gameplay.DirectionsSystem.Waypoints
{
	public abstract class AbstractWayPoint : BetterMonoBehaviour
	{
		public static readonly List<AbstractWayPoint> AllWaypoints = new List<AbstractWayPoint>();
		
		public Vector3 Position => transform.position;

		protected AbstractWayPoint[] connections;

		protected virtual void Awake()
		{
			AllWaypoints.Add(this);
		}
		
		public AbstractWayPoint[] GetConnections()
		{
			return connections;
		}

		public AbstractWayPoint GetFirstConnection()
		{
			return connections[0];
		}

		public static AbstractWayPoint GetClosestWayPoint(Vector3 position)
		{
			float closestDistance = float.PositiveInfinity;
			AbstractWayPoint closestWayPoint = null;

			foreach (AbstractWayPoint wayPoint in AllWaypoints)
			{
				float distance = (position - wayPoint.Position).sqrMagnitude;

				if (distance < closestDistance)
				{
					closestDistance = distance;
					closestWayPoint = wayPoint;
				}
			}

			return closestWayPoint;
		}

		private void OnDestroy()
		{
			AllWaypoints.Remove(this);
		}
	}
}