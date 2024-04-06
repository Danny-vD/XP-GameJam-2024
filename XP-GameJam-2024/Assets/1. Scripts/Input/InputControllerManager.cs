using System;
using System.Collections.Generic;
using System.Linq;
using Input.Enum;
using UnityEngine.InputSystem;
using VDFramework.Singleton;

namespace Input
{
    public class InputControllerManager : Singleton<InputControllerManager>
    {
        private Dictionary<ControlTypes, InputActionMap> actionMapsByType;

        private ControlTypes beforeMenu;
        private ControlTypes currentType;
        
        public MainInput mainInput;
        
        public event Action OnMenuOpened = delegate { };
        public event Action OnMenuClosed = delegate { };

        protected override void Awake()
        {
            mainInput = new MainInput();
            
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
            
            actionMapsByType = new Dictionary<ControlTypes, InputActionMap>
            {
                //TODO: Automate this
                { ControlTypes.Menus, mainInput.Menu.Get() },
                { ControlTypes.Overworld, mainInput.Overworld.Get() },
                { ControlTypes.Special, mainInput.Special.Get() },
                
                // { ControlTypes.Combat, playerControls.Combat.Get() },
                // { ControlTypes.Dialogue, playerControls.Dialogue.Get() }
            };
            
            //actionMapsByType.First().Value.Enable();
            //currentType = actionMapsByType.First().Key;
            
            ChangeControls(ControlTypes.Overworld);

            base.Awake();
        }
        
        public void ChangeControls(ControlTypes type)
        {
            if (type == ControlTypes.Menus)
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
    }
}