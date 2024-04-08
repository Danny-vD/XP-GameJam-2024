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
            EventManager.AddListener<VillagerSavedEvent>(OnVillagerSaved, -10);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<VillagerSavedEvent>(OnVillagerSaved);
        }

        private void OnVillagerSaved(VillagerSavedEvent @event)
        {
            label.text = "Villagers Saved: " + GameManager.Instance.saved;
        }
        
        
    }
}