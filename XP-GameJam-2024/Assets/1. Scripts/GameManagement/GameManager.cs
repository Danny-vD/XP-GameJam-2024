using GameManagement.Events;
using UnityEngine;
using VDFramework.EventSystem;
using VDFramework.Singleton;

namespace GameManagement
{
    public class GameManager : Singleton<GameManager>
    {

        [SerializeField] private GameObject timer;
        public int saved;
        public int dead;

        protected override void Awake()
        {
            base.Awake();
            saved = 0;
            dead = 0;
            EventManager.AddListener<VillagerSaveEvent>(OnVillagerSaved, 100);
            EventManager.AddListener<VillagerDeathEvent>(OnVillagerDeath, 100);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<VillagerSaveEvent>(OnVillagerSaved);
        }

        private void OnVillagerSaved(VillagerSaveEvent @event)
        {
            saved = saved + 1;
        }

        private void OnVillagerDeath(VillagerDeathEvent @event)
        {
            dead = dead + 1;

            // if (dead >= )
            // {
            //     
            // }
        }

        private void CheckVillagers()
        {
            // FindObjectsByType<>()
        }
        public void GameRewind()
        {
            
        }
         
    }
}