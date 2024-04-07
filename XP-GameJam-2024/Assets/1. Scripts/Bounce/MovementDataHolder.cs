using UnityEngine;
using VDFramework;

namespace PhysicsScripts.Movement
{
	public class MovementDataHolder : BetterMonoBehaviour
	{
		[field: SerializeField]
		public Vector2 Speed { get; set; }
		
		private Vector2 movementDirection;
		
		public Vector2 Direction
		{
			get => movementDirection;
			set => movementDirection = value.normalized;
		}
		
		public Vector2 Velocity => Time.deltaTime * Speed * Direction;
	}
}