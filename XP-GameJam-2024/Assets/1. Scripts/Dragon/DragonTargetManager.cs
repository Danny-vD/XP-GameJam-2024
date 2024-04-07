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

		private void Start()
		{
			VillagerDeathEvent.AddListener(GetAllPossibleTargets, -100);
			VillagerSaveEvent.AddListener(GetAllPossibleTargets, -100);
		}

		public void SetNewTarget()
		{
			if (GetRandomTarget(out Villager target))
			{
				CurrentTarget = target;
				return;
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