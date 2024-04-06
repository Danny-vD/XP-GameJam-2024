using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using VDFramework;
using VDFramework.EventSystem;

namespace PlayerMovement
{
    
    public class PlayerMovement : BetterMonoBehaviour
    {
        
        [SerializeField] [Tooltip("The speed of the player in m/s")]
        private float speed = 10;

        private CharacterController controller;
        private Vector3 deltaMovement;
        private bool isMoving;

        private void Awake()
        {
            
            controller = GetComponent<CharacterController>();
            InputControllerManager.Instance.mainInput.Overworld.Movement.performed += MovementOnPerformed;
            InputControllerManager.Instance.mainInput.Overworld.Movement.canceled += MovementOnCanceled;
        }

        private void OnDestroy()
        {
            InputControllerManager.Instance.mainInput.Overworld.Movement.performed -= MovementOnPerformed;
            InputControllerManager.Instance.mainInput.Overworld.Movement.canceled -= MovementOnCanceled;

            // InputControllerManager.Instance.mainInput.Overworld.Interact.performed += OnInteract;
            // InputControllerManager.Instance.mainInput.Overworld.Select.performed += OnSelect;
            // InputControllerManager.Instance.mainInput.Overworld.Start.performed += OnStart;
        }

        private void MovementOnPerformed(InputAction.CallbackContext obj)
        {
            isMoving = true;
            var vector = obj.ReadValue<Vector2>();
            deltaMovement = new Vector3(vector.x, 0, vector.y);
        }

        private void MovementOnCanceled(InputAction.CallbackContext obj)
        {
            isMoving = false;
        }
        
        private void Update()
        {
            // TODO: Change this to co-routine
            if (!isMoving) return;
            var delta = speed * deltaMovement;
            controller.SimpleMove(delta);
        }
    }
}