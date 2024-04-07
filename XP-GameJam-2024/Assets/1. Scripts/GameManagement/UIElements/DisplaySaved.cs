using System;
using GameManagement.Events;
using TMPro;
using VDFramework;
using VDFramework.EventSystem;

namespace GameManagement.UIElements
{
    public class DisplaySaved : BetterMonoBehaviour
    {
        private TMP_Text label;
        
        private void Awake()
        {
            label = GetComponent<TMP_Text>();
            EventManager.AddListener<VillagerSaveEvent>(OnVillagerSaved, -10);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<VillagerSaveEvent>(OnVillagerSaved);
        }

        private void OnVillagerSaved(VillagerSaveEvent @event)
        {
            label.text = "Villagers Saved: " + GameManager.Instance.saved;
        }
        
        
    }
}