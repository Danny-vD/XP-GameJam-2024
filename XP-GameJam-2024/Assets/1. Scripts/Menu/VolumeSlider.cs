using FMODUtilityPackage.Core;
using FMODUtilityPackage.Enums;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace Menu
{
	[DisallowMultipleComponent, RequireComponent(typeof(Slider))]
	public class VolumeSlider : BetterMonoBehaviour
	{
		[SerializeField]
		private BusType audioBus;

		[SerializeField]
		private Vector2 minMaxVolume = new Vector2(0, 1);
		
		private Slider slider;

		private void Awake()
		{
			slider = GetComponent<Slider>();
		}

		private void OnEnable()
		{
			slider.value = AudioManager.Instance.GetVolume(audioBus);
			slider.onValueChanged.AddListener(SetVolume);
		}

		private void OnDisable()
		{
			slider.onValueChanged.RemoveListener(SetVolume);
		}

		private void SetVolume(float value)
		{
			AudioManager.Instance.SetVolume(audioBus, Mathf.Lerp(minMaxVolume.x, minMaxVolume.y, value));
		}
	}
}
