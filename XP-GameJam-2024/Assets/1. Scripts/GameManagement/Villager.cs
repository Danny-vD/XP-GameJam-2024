using GameManagement.Events;
using VDFramework;
using VDFramework.EventSystem;

namespace GameManagement
{
    public class Villager : BetterMonoBehaviour
    {
        public void Kill()
        {
            Destroy(gameObject);
            
            EventManager.RaiseEvent(new VillagerDeathEvent());
        }
    }
}