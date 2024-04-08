using FMODUtilityPackage.Core;
using FMODUtilityPackage.Enums;
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
			
			EventManager.RaiseEvent(new VillagerDeathEvent(this));

			AudioPlayer.PlayOneShot2D(AudioEventType.Sound_Effects_NPCs_npcDeath);
		}
	}
}