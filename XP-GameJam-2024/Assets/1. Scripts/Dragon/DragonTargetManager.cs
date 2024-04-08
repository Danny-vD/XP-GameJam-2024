using System.Collections.Generic;
using System.Linq;
using GameManagement;
using GameManagement.Events;
using UnityEngine;
using VDFramework;
using VDFramework.Extensions;

namespace Dragon
{
	public class DragonTargetManager : BetterMonoBehaviour
	{
		private List<Villager> targets;

		public Villager CurrentTarget { get; private set; }
		public bool HasValidTarget => CurrentTarget != null;
		
		public bool TargetsAvailable => targets.Count > 0;

		private void Awake()
		{
			GetAllPossibleTargets();
		}

		private void Start()
		{
			VillagerDeathEvent.AddListener(UpdatePossibleTargets, -100);
			VillagerSavedEvent.AddListener(UpdatePossibleTargets, -100);
		}

		public void SetNewTarget()
		{
			if (CurrentTarget == null)
			{
				targets.Remove(CurrentTarget);
			}
			
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
			targets = FindObjectsByType<Villager>(FindObjectsSortMode.None).ToList();
		}

		private void UpdatePossibleTargets(VillagerDeathEvent villagerDeathEvent)
		{
			targets.Remove(villagerDeathEvent.KilledVillager);
		}

		private void UpdatePossibleTargets(VillagerSavedEvent villagerSavedEvent)
		{
			targets.Remove(villagerSavedEvent.SavedVillager);
		}

		private void OnDestroy()
		{
			VillagerDeathEvent.RemoveListener(GetAllPossibleTargets);
			VillagerSavedEvent.RemoveListener(GetAllPossibleTargets);
		}
	}
}