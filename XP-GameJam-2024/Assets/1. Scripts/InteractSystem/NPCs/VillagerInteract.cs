using System;
using InteractSystem.Interfaces;
using UnityEngine;
using VDFramework;

namespace InteractSystem.NPCs
{
	[DisallowMultipleComponent]
	public class VillagerInteract : BetterMonoBehaviour, IInteractable
	{
		public event Action OnInteract = delegate { };

		public void Interact()
		{
			OnInteract.Invoke();
		}
	}
}