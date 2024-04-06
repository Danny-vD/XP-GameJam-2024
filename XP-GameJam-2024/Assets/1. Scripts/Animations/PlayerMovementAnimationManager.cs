using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using VDFramework;

namespace Animations
{
	[RequireComponent(typeof(Animator))]
	public class PlayerMovementAnimationManager : BetterMonoBehaviour
	{
		private Animator animator;
		private static readonly int walking = Animator.StringToHash("Walking");

		private void Awake()
		{
			animator = GetComponent<Animator>();
		}

		private void OnEnable()
		{
			InputControllerManager.Instance.mainInput.Overworld.Movement.performed -= StartMoving;
			InputControllerManager.Instance.mainInput.Overworld.Movement.canceled  -= StopMoving;
		}

		private void OnDisable()
		{
			InputControllerManager.Instance.mainInput.Overworld.Movement.performed -= StartMoving;
			InputControllerManager.Instance.mainInput.Overworld.Movement.canceled  -= StopMoving;
		}

		private void StartMoving(InputAction.CallbackContext obj)
		{
			animator.SetBool(walking, true);
		}
		
		private void StopMoving(InputAction.CallbackContext obj)
		{
			animator.SetBool(walking, false);
		}
	}
}