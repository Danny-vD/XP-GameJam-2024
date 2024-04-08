using VDFramework.EventSystem;

namespace GameManagement.Events
{
    public class VillagerSavedEvent : VDEvent<VillagerSavedEvent>
    {
        public readonly Villager SavedVillager;

        public VillagerSavedEvent(Villager savedVillager)
        {
            SavedVillager = savedVillager;
        }
    }
}