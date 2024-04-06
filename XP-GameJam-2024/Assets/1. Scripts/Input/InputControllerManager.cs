using System.Collections.Generic;
using System.Linq;
using Input.Enum;
using SerializableDictionaryPackage.SerializableDictionary;
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
		
		public ControlType CurrentControlType { get; private set; }
		
		private SerializableDictionary<ControlType, InputActionMap> actionMapsByType;

		protected override void Awake()
		{
			base.Awake();

			CurrentControlType = default;
			ChangeControls(default);
		}

		public void ChangeControls(ControlType type)
		{
			if (type == CurrentControlType)
			{
				return;
			}
			
			actionMapsByType[CurrentControlType].Disable();
			actionMapsByType[type].Enable();
			CurrentControlType = type;
		}

		public InputActionMap GetActionMap(ControlType controlType)
		{
			return actionMapsByType[controlType];
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