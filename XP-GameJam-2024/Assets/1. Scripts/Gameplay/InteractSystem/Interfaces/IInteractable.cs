using System;

namespace Gameplay.InteractSystem.Interfaces
{
	public interface IInteractable
	{
		event Action OnInteract;

		bool CanInteract { get; }

		void Interact();
	}
}