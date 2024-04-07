using System.Linq;
using GameManagement.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using VDFramework.EventSystem;
using VDFramework.Singleton;

namespace GameManagement
{
    public class GameManager : Singleton<GameManager>
    {

        [SerializeField] private GameObject anim;
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
            CheckVillagers();
        }

        private void OnVillagerDeath(VillagerDeathEvent @event)
        {
            dead = dead + 1;

            CheckVillagers();
        }

        private void CheckVillagers()
        {
            if (FindObjectsByType<Villager>(FindObjectsSortMode.None).Length <= 0)
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