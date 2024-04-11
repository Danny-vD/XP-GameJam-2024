using System.Collections.Generic;
using Gameplay.DirectionsSystem.NPCs;
using Gameplay.InteractSystem.Player;
using Input.Enum;
using Input.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using VDFramework;
using VDFramework.EventSystem;

namespace Input
{
	public class InputSwitcher : BetterMonoBehaviour
	{
		[SerializeField]
		private InputActionReference specialInteractInput;
		
		private PlayerInteract playerInteract;

		private void Awake()
		{
			playerInteract = GetComponent<PlayerInteract>();
		}

		private void OnEnable()
		{
			specialInteractInput.action.performed  += SwitchToNormalInput;
			playerInteract.OnInteractWithDirectionReceivers += SwitchToSpecialInput;
		}

		private void OnDisable()
		{
			specialInteractInput.action.performed  -= SwitchToNormalInput;
			playerInteract.OnInteractWithDirectionReceivers -= SwitchToSpecialInput;
		}

		private static void SwitchToNormalInput(InputAction.CallbackContext context)
		{
			SwitchToNormalInput();
		}
		
		private static void SwitchToNormalInput()
		{
			InputControllerManager.Instance.ChangeControls(ControlType.Overworld);
			EventManager.RaiseEvent(new SwitchToNormalInputEvent());
		}

		private static void SwitchToSpecialInput(List<DirectionsReceiver> villagers)
		{
			InputControllerManager.Instance.ChangeControls(ControlType.Special);
			EventManager.RaiseEvent(new SwitchToSpecialInputEvent());
		}
	}
}