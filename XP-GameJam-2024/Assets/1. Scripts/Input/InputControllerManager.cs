using System;
using System.Collections.Generic;
using Input.Enum;
using SerializableDictionaryPackage.SerializableDictionary;
using UnityEngine;
using UnityEngine.InputSystem;
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
                { ControlType.Menus, mainInput.Menu.Get() },
                { ControlType.Overworld, mainInput.Overworld.Get() },
                { ControlType.Special, mainInput.Special.Get() },
                
                // { ControlTypes.Combat, playerControls.Combat.Get() },
                // { ControlTypes.Dialogue, playerControls.Dialogue.Get() }
            };
            
            //actionMapsByType.First().Value.Enable();
            //currentType = actionMapsByType.First().Key;
            
            ChangeControls(ControlType.Overworld);

            base.Awake();
        }
        
        public void ChangeControls(ControlType type)
        {
            if (type == ControlType.Menus)
            {
                beforeMenu = currentType;
                OpenSettings();
            }
            else
            {
                CloseSettings();
            }
            
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
        /*
         * Get a reference to the InputActionAsset
         *
         * Use myAsset.actionsMaps to get all the action maps
         *
         * Then using a for-loop assign each ActionMap to an Enum Value (the enum value won't exist yet, so cast the index to the ControlType enum)
         * store the name of the actionMap to use as the enum value afterwards (use an array)
         *
         * write your array of names to the enum using the enum writer
         *
         * Optionally use a dictionary of <TEnum,InputActionAssets> to map your assets to a specific enum as well
         */
        
        private void CreateEnums()
        {
            int enumValue = 0;
            List<string> enumNames = new List<string>();
            
            foreach (InputAction inputAction in inputActionAsset)
            {
                ControlType controlType = (ControlType)enumValue;
            }
        }
#endif
    }
}