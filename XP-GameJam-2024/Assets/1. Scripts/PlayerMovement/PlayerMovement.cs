using System;
using System.Collections;
using Input;
using MapMovement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using VDFramework;

namespace PlayerMovement
{
	public class PlayerMovement : BetterMonoBehaviour, IActorMover
	{
		public event Action OnMovementStart;
		public event Action OnMovementCancelled;
		public event Action OnEnterIdle;
		
		[SerializeField]
		[Tooltip("The speed of the player in m/s")]
		private float speed = 10;

		private NavMeshAgent agent;
		private Vector3 deltaMovement;
		private bool isMoving;

		private Coroutine moveCoroutine = null;

		private void Awake()
		{
			agent = GetComponent<NavMeshAgent>();

			InputControllerManager.Instance.mainInput.Overworld.Movement.performed += MovementOnPerformed;
			InputControllerManager.Instance.mainInput.Overworld.Movement.canceled  += MovementOnCanceled;
		}

		private void OnDestroy()
		{
			if (InputControllerManager.IsInitialized)
			{
				InputControllerManager.Instance.mainInput.Overworld.Movement.performed -= MovementOnPerformed;
				InputControllerManager.Instance.mainInput.Overworld.Movement.canceled  -= MovementOnCanceled;

				// InputControllerManager.Instance.mainInput.Overworld.Interact.performed += OnInteract;
				// InputControllerManager.Instance.mainInput.Overworld.Select.performed += OnSelect;
				// InputControllerManager.Instance.mainInput.Overworld.Start.performed += OnStart;
			}
		}

		private void MovementOnPerformed(InputAction.CallbackContext obj)
		{
			isMoving = true;

			Vector2 vector = obj.ReadValue<Vector2>();
			deltaMovement = new Vector3(vector.x, 0, vector.y);

			moveCoroutine ??= StartCoroutine(MovePlayer());
		}

		private void MovementOnCanceled(InputAction.CallbackContext obj)
		{
			isMoving = false;
		}

		private IEnumerator MovePlayer()
		{
			do
			{
				yield return null;

				Vector3 delta = Time.deltaTime * speed * deltaMovement;
				agent.Move(delta);
			} while (isMoving);

			moveCoroutine = null;
		}
	}
}