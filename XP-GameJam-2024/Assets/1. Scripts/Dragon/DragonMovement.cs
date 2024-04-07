using System;
using UnityEngine;
using VDFramework;

namespace Dragon
{
	public class DragonMovement : BetterMonoBehaviour
	{
		[SerializeField]
		private float speed = 5.0f;

		[SerializeField]
		private float distanceFromTarget = 1.5f;

		private DragonTargetManager dragonTargetManager;

		private void Awake()
		{
			dragonTargetManager = GetComponent<DragonTargetManager>();
		}

		private void Start()
		{
			if (dragonTargetManager.TargetsAvailable)
			{
				dragonTargetManager.SetNewTarget();
			}
		}

		private void LateUpdate()
		{
			if (dragonTargetManager.HasValidTarget)
			{
				Vector3 currentPosition = transform.position;
				Vector3 targetPosition = dragonTargetManager.CurrentTarget.transform.position + Vector3.right * distanceFromTarget;

				Vector3 towardsTarget = (targetPosition - currentPosition).normalized;

				currentPosition += speed * Time.deltaTime * towardsTarget;

				transform.position = currentPosition;
			}
		}
	}
}