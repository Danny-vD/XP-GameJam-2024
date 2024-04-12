using Input.Events;
using UnityEngine;
using VDFramework;

namespace CameraScripts
{
	public class CameraSwitcher : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject camera1;
		
		[SerializeField]
		private GameObject camera2;

		private void OnEnable()
		{
			SwitchToNormalInputEvent.AddListener(SwitchToCamera1);
			SwitchToSpecialInputEvent.AddListener(SwitchToCamera2);
		}

		private void OnDisable()
		{
			SwitchToNormalInputEvent.RemoveListener(SwitchToCamera1);
			SwitchToSpecialInputEvent.RemoveListener(SwitchToCamera2);
		}

		private void SwitchToCamera1()
		{
			camera1.SetActive(true);
			camera2.SetActive(false);
		}

		private void SwitchToCamera2()
		{
			camera1.SetActive(false);
			camera2.SetActive(true);
		}
	}
}