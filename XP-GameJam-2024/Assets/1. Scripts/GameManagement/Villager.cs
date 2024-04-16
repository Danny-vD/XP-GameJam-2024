using System;
using FMODUtilityPackage.Core;
using FMODUtilityPackage.Enums;
using GameManagement.Events;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace GameManagement
{
	[DisallowMultipleComponent]
	public class Villager : BetterMonoBehaviour
	{
		public event Action OnDeath;

		public void Kill()
		{
			Destroy(gameObject);
			
			EventManager.RaiseEvent(new VillagerDeathEvent(this));

			AudioPlayer.PlayOneShot2D(AudioEventType.Sound_Effects_NPCs_npcDeath);
			
			OnDeath?.Invoke();
			OnDeath = null;
		}
	}
}