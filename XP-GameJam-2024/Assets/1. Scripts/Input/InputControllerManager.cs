using System.Collections.Generic;
using System.Linq;
using FMODUtilityPackage.Core;
using Input.Enum;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using VDFramework.Extensions;
using VDFramework.Singleton;

namespace Input
{
	public class InputControllerManager : Singleton<InputControllerManager>
	{
		[SerializeField]
		private InputActionAsset inputActionAsset;

		[SerializeField]
		private InputActionReference specialInteraction;

		[SerializeField]
		private InputActionReference overworldInteraction;

		private readonly Dictionary<ControlType, InputActionMap> actionMapsByType = new Dictionary<ControlType, InputActionMap>();

		public ControlType CurrentControlType { get; private set; }

		private FMOD.Studio.EventInstance playSlowdown;

		protected override void Awake()
		{
			base.Awake();
			
			AudioParameterManager.SetGlobalParameter("TimeSlowed", 0);
			playSlowdown = AudioPlayer.GetEventInstance(FMODUtilityPackage.Enums.AudioEventType.Sound_Effects_Time_TimeSlowdown);

			SetActionsMapsPerControlType();

			specialInteraction.action.performed   += OnSpecialInteraction;
			overworldInteraction.action.performed += OnOverworldInteraction;

			CurrentControlType = default;
			SetControls(default);
		}

		protected override void OnDestroy()
		{
			specialInteraction.action.performed   -= OnSpecialInteraction;
			overworldInteraction.action.performed -= OnOverworldInteraction;
			base.OnDestroy();
		}

		private void OnOverworldInteraction(InputAction.CallbackContext obj)
		{
			// Trigger time dilation
			Time.timeScale = 0.2f;
			AudioParameterManager.SetGlobalParameter("TimeSlowed", 1); // TODO Don't do this here
			playSlowdown.start();
			
			ChangeControls(ControlType.Special);
		}

		private void OnSpecialInteraction(InputAction.CallbackContext obj)
		{
			//Trigger time dilation
			Time.timeScale = 1.0f;
			AudioParameterManager.SetGlobalParameter("TimeSlowed", 0); // TODO Don't do this here
			
			ChangeControls(ControlType.Overworld);
		}

		public void ChangeControls(ControlType controlType)
		{
			if (controlType == CurrentControlType)
			{
				return;
			}

			SetControls(controlType);
		}

		public InputActionMap GetActionMap(ControlType controlType)
		{
			return actionMapsByType[controlType];
		}

		private void SetControls(ControlType controlType)
		{
			actionMapsByType[CurrentControlType].Disable();
			actionMapsByType[controlType].Enable();
			CurrentControlType = controlType;
		}

		private void SetActionsMapsPerControlType()
		{
			IEnumerator<ControlType> controlTypes = default(ControlType).GetValues().GetEnumerator();

			foreach (InputActionMap inputActionMap in inputActionAsset.actionMaps)
			{
				if (!controlTypes.MoveNext())
				{
					break;
				}

				actionMapsByType[controlTypes.Current] = inputActionMap;
			}

			controlTypes.Dispose();
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (inputActionAsset != null)
			{
				CreateEnums();
			}
		}

		private void CreateEnums()
		{
			if (default(ControlType).GetValues().Count() == inputActionAsset.actionMaps.Count)
			{
				return;
			}

			int enumValue = 0;
			List<string> enumNames = new List<string>();

			foreach (InputActionMap inputActionMap in inputActionAsset.actionMaps)
			{
				ControlType controlType = (ControlType)enumValue;

				actionMapsByType[controlType] = inputActionMap;

				enumNames.Add(inputActionMap.name);

				++enumValue;
			}

			EnumWriter.WriteToEnum<ControlType>(enumNames, null);
		}
#endif
	}
}