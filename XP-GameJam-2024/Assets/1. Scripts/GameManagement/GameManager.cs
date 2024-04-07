using GameManagement.Events;
using UnityEngine;
using VDFramework.EventSystem;
using VDFramework.Singleton;

namespace GameManagement
{
    public class GameManager : Singleton<GameManager>
    {
        public int saved;
        
        private void Awake()
        {
            base.Awake();
            saved = 0;
            EventManager.AddListener<VillagerSaveEvent>(OnVillagerSaved, 100);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<VillagerSaveEvent>(OnVillagerSaved);
        }

        private void OnVillagerSaved(VillagerSaveEvent @event)
        {
            saved = saved + 1;
        }
         
    }
}