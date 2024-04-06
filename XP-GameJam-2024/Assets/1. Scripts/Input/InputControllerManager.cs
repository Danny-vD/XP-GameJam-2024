using System;
using System.Collections.Generic;
using Input.Enum;
using SerializableDictionaryPackage.SerializableDictionary;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using VDFramework.Singleton;

namespace Input
{
	public class InputControllerManager : Singleton<InputControllerManager>
	{
		[SerializeField]
		private SerializableDictionary<ControlType, InputActionMap> actionMapsByType;

		private ControlType beforeMenu;
		private ControlType currentType;

		public MainInput mainInput;

		[SerializeField]
		private InputActionAsset inputActionAsset;

		public event Action OnMenuOpened = delegate { };
		public event Action OnMenuClosed = delegate { };

		protected override void Awake()
		{
			mainInput = new MainInput();

			actionMapsByType = new Dictionary<ControlType, InputActionMap>
			{
				//TODO: Automate this
				//{ ControlType.Menus, mainInput.Menu.Get() },
				//{ ControlType.Overworld, mainInput.Overworld.Get() },
				//{ ControlType.Special, mainInput.Special.Get() },

				// { ControlTypes.Combat, playerControls.Combat.Get() },
				// { ControlTypes.Dialogue, playerControls.Dialogue.Get() }
			};

			//actionMapsByType.First().Value.Enable();
			//currentType = actionMapsByType.First().Key;

			//ChangeControls(ControlType.Overworld);

			base.Awake();
		}

		public void ChangeControls(ControlType type)
		{
			//if (type == ControlType.Menus)
			//{
			//	beforeMenu = currentType;
			//	OpenSettings();
			//}
			//else
			//{
			//	CloseSettings();
			//}

			actionMapsByType[currentType].Disable();
			actionMapsByType[type].Enable();
			currentType = type;
		}

		public void ReturnToPreviousControls()
		{
			actionMapsByType[currentType].Disable();
			actionMapsByType[beforeMenu].Enable();

			currentType = beforeMenu;
			CloseSettings();
		}

		private void OpenSettings()
		{
			OnMenuOpened?.Invoke();
		}

		private void CloseSettings()
		{
			OnMenuClosed?.Invoke();
		}

#if UNITY_EDITOR
		[ContextMenu("Test")]
		private void CreateEnums()
		{
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