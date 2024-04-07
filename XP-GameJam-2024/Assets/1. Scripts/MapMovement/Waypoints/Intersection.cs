using System.Collections.Generic;
using MapMovement.Enums;
using SerializableDictionaryPackage.SerializableDictionary;
using UnityEngine;
using VDFramework;

namespace MapMovement.Waypoints
{
	[ExecuteInEditMode]
	public class Intersection : BetterMonoBehaviour
	{
		[SerializeField]
		private SerializableEnumDictionary<Direction, Intersection> directionalConnections;
		
		[SerializeField]
		public List<Intersection> Connections;

		public Intersection GetConnectingIntersection(Direction direction)
		{
			return directionalConnections[direction];
		}

#if UNITY_EDITOR
		private void Update()
		{
			Debug.DrawLine(transform.position, (transform.position + new Vector3(Vector2.up.x * 3, 0, Vector2.up.y * 3)));

			foreach (Intersection intersection in Connections)
			{
				if (intersection == null)
				{
					Debug.LogWarning("A connection is not assigned!", this);
					continue;
				}

				Debug.DrawLine(transform.position, intersection.transform.position, Color.red);
			}
		}
#endif
	}
}