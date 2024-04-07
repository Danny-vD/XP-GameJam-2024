using System.Collections.Generic;
using MapMovement.Enums;
using PhysicsScripts.Movement.Baseclasses;
using UnityEngine;

using VDFramework.Extensions;

namespace PhysicsScripts.Movement.MovementImplementations
{
	public class RandomStartMovement : AbstractUnitMovement
	{
		[SerializeField]
		private Vector2 minMaxAngle = new Vector2(5, 85);

		[SerializeField]
		private List<Direction> startDirections;

		protected override void Awake()
		{
			base.Awake();
			MovementDataHolder.Direction = GetRandomDirection();
		}

		private Vector2 GetRandomDirection()
		{
			float randomAngle = Random.Range(minMaxAngle.x, minMaxAngle.y);

			Vector2 direction = startDirections.GetRandomElement() switch
			{
				Direction.Up => Vector2.up,
				Direction.Right => Vector2.right,
				Direction.Down => Vector2.down,
				Direction.Left => Vector2.left,
				_ => MovementDataHolder.Direction,
			};

			direction = Quaternion.Euler(0, 0, randomAngle) * direction;
			return direction;
		}

#if UNITY_EDITOR

		[SerializeField]
		private bool visualizeArc;

		[SerializeField]
		private Color visualisationColor = new Color(0.2f, 0.6f, 0.2f, 1);

		private void OnEnable()
		{
			if (visualizeArc)
			{
				UnityEditor.SceneView.duringSceneGui += DrawHandles;
			}
		}

		private void OnDisable()
		{
			UnityEditor.SceneView.duringSceneGui -= DrawHandles;
		}

		private void DrawHandles(UnityEditor.SceneView sceneView)
		{
			float angle = minMaxAngle.y - minMaxAngle.x;

			UnityEditor.Handles.color = visualisationColor;
			Quaternion minAngleRotation = Quaternion.Euler(0, 0, minMaxAngle.x);

			if (startDirections.Contains(Direction.Up))
			{
				UnityEditor.Handles.DrawSolidArc(Vector3.zero, Vector3.forward, minAngleRotation * Vector3.up, angle, 5);
			}

			if (startDirections.Contains(Direction.Right))
			{
				UnityEditor.Handles.DrawSolidArc(Vector3.zero, Vector3.forward, minAngleRotation * Vector3.right, angle, 5);
			}

			if (startDirections.Contains(Direction.Down))
			{
				UnityEditor.Handles.DrawSolidArc(Vector3.zero, Vector3.forward, minAngleRotation * Vector3.down, angle, 5);
			}

			if (startDirections.Contains(Direction.Left))
			{
				UnityEditor.Handles.DrawSolidArc(Vector3.zero, Vector3.forward, minAngleRotation * Vector3.left, angle, 5);
			}
		}
#endif
	}
}