
using UnityEngine;
using VDFramework.Utility;

namespace PhysicsScripts.Movement.Bouncing
{
	public class BasicBounceManager : AbstractBounceManager
	{
		[SerializeField, Tooltip("The chance for the angle to change on a bounce")]
		private int randomAngleChance = 50;

		[SerializeField, Tooltip("The amount the angle changes by")]
		private float randomAngle = 1;

		private Quaternion angleDelta = Quaternion.identity;
		private Quaternion negativeAngleDelta = Quaternion.identity;

		protected override void Awake()
		{
			base.Awake();
			
			angleDelta         = Quaternion.Euler(0, 0, randomAngle);
			negativeAngleDelta = Quaternion.Inverse(angleDelta);
		}

		public void ReflectOffSurface(Vector2 normal)
		{
			Vector2 newDirection = Vector2.Reflect(MovementDataHolder.Direction, normal);

			if (Random.Range(1, 101) <= randomAngleChance)
			{
				if (RandomUtil.RandomBool())
				{
					newDirection = angleDelta * newDirection;
				}
				else
				{
					newDirection = negativeAngleDelta * newDirection;
				}
			}

			MovementDataHolder.Direction = newDirection;
			
			RaiseBounceEvent();
		}

		public void ReflectOffSurface(Vector2 normal, float angle)
		{
			Vector2 newDirection = Quaternion.Euler(0, 0, angle) * normal;

			if (Random.Range(1, 101) <= randomAngleChance)
			{
				if (RandomUtil.RandomBool())
				{
					newDirection = angleDelta * newDirection;
				}
				else
				{
					newDirection = negativeAngleDelta * newDirection;
				}
			}

			MovementDataHolder.Direction = newDirection;
			
			RaiseBounceEvent();
		}
	}
}