using System;

namespace InteractSystem.Interfaces
{
	public interface IInteractable
	{
		event Action OnInteract;

		void Interact();
	}
}