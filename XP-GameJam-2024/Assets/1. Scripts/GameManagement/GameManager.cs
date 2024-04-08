using GameManagement.Events;
using UnityEngine;
using VDFramework.EventSystem;
using VDFramework.Singleton;

namespace GameManagement
{
	public class GameManager : Singleton<GameManager>
	{
		[SerializeField]
		private GameObject anim;

		public int saved;
		public int dead;

		protected override void Awake()
		{
			base.Awake();
			saved = 0;
			dead  = 0;
			EventManager.AddListener<VillagerSavedEvent>(OnVillagerSaved, 100);
			EventManager.AddListener<VillagerDeathEvent>(OnVillagerDeath, 100);
		}

		private void OnDisable()
		{
			EventManager.RemoveListener<VillagerSavedEvent>(OnVillagerSaved);
			EventManager.RemoveListener<VillagerDeathEvent>(OnVillagerDeath);
		}

		private void OnVillagerSaved(VillagerSavedEvent @event)
		{
			saved = saved + 1;
			CheckVillagers(@event.SavedVillager); // Ignore the villager that just got saved
		}

		private void OnVillagerDeath(VillagerDeathEvent @event)
		{
			dead = dead + 1;

			CheckVillagers(@event.KilledVillager); // Ignore the villager that just died
		}

		private void CheckVillagers(Villager ignoreVillager = null)
		{
			int villagerCount = FindObjectsByType<Villager>(FindObjectsSortMode.None).Length;

			if (!ReferenceEquals(ignoreVillager, null))
			{
				--villagerCount; // Ignoring 1 villager
			}

			if (villagerCount <= 0)
			{
				GameRewind();
			}
		}

		public void GameRewind()
		{
			anim.SetActive(true);
		}
	}
}