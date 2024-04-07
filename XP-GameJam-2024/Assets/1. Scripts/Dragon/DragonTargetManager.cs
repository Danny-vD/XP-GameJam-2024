using System.Collections;
using GameManagement;
using GameManagement.Events;
using UnityEngine;
using VDFramework;
using VDFramework.Extensions;

namespace Dragon
{
	public class DragonTargetManager : BetterMonoBehaviour
	{
		private Villager[] targets;

		public Villager CurrentTarget { get; private set; }
		public bool HasValidTarget => CurrentTarget != null;
		public bool TargetsAvailable => targets.Length > 0;

		private void Awake()
		{
			GetAllPossibleTargets();
		}

		private IEnumerator Start()
		{
			VillagerDeathEvent.AddListener(GetAllPossibleTargets, -100);
			VillagerSaveEvent.AddListener(GetAllPossibleTargets, -100);

			yield return new WaitForSeconds(1.5f);
			
			GetAllPossibleTargets();
		}

		public void SetNewTarget()
		{
			if (GetRandomTarget(out Villager target))
			{
				CurrentTarget = target;
			}

			CurrentTarget = null;
		}

		private bool GetRandomTarget(out Villager target)
		{
			target = targets.GetRandomElement();

			return target != null;
		}

		private void GetAllPossibleTargets()
		{
			targets = FindObjectsByType<Villager>(FindObjectsSortMode.None);
		}

		private void OnDestroy()
		{
			VillagerDeathEvent.RemoveListener(GetAllPossibleTargets);
			VillagerSaveEvent.RemoveListener(GetAllPossibleTargets);
		}
	}
}