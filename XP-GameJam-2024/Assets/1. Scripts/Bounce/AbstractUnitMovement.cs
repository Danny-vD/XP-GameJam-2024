using UnityEngine;
using VDFramework;

namespace PhysicsScripts.Movement.Baseclasses
{
	[RequireComponent(typeof(MovementDataHolder))]
	public abstract class AbstractUnitMovement : BetterMonoBehaviour
	{
		protected MovementDataHolder MovementDataHolder;

		private Rigidbody2D rigidbdy;

		protected virtual void Awake()
		{
			rigidbdy           = GetComponent<Rigidbody2D>();
			MovementDataHolder = GetComponent<MovementDataHolder>();
		}

		private void FixedUpdate()
		{
			Vector2 delta = MovementDataHolder.Velocity;
			rigidbdy.MovePosition(rigidbdy.position + delta);
		}
	}
}