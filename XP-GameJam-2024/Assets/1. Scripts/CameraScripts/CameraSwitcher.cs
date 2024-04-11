using UnityEngine;
using UnityEngine.InputSystem;
using VDFramework;

namespace CameraScripts
{
	public class CameraSwitcher : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject camera1;
		
		[SerializeField]
		private GameObject camera2;

		[SerializeField]
		private InputActionReference overworldInteract; // switches to camera 2
		
		[SerializeField]
		private InputActionReference specialInteract; // switches to camera 1

		private void Awake()
		{
			overworldInteract.action.performed += SwitchToCamera2;
			specialInteract.action.performed   += SwitchToCamera1;
		}

		private void SwitchToCamera1(InputAction.CallbackContext obj)
		{
			camera1.SetActive(true);
			camera2.SetActive(false);
		}

		private void SwitchToCamera2(InputAction.CallbackContext obj)
		{
			camera1.SetActive(false);
			camera2.SetActive(true);
		}

		private void OnDestroy()
		{
			overworldInteract.action.performed -= SwitchToCamera2;
			specialInteract.action.performed   -= SwitchToCamera1;
		}
	}
}