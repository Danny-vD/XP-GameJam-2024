using System.Collections;
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

		[SerializeField]
		private float distanceForKill = 1;

		[SerializeField]
		private float secondsWaitAfterKill = 1.5f;

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

				Vector3 towardsTarget = targetPosition - currentPosition;

				currentPosition += speed * Time.deltaTime * towardsTarget.normalized;

				transform.position = currentPosition;

				float distance = towardsTarget.magnitude;
				
				if (distance <= distanceForKill)
				{
					dragonTargetManager.CurrentTarget.Kill();

					StartCoroutine(FindNewTarget());
				}
			}
			else
			{
				dragonTargetManager.SetNewTarget();
			}
		}

		private IEnumerator FindNewTarget()
		{
			yield return new WaitForSeconds(secondsWaitAfterKill);
			
			if (dragonTargetManager.TargetsAvailable)
			{
				dragonTargetManager.SetNewTarget();
			}
		}
	}
}