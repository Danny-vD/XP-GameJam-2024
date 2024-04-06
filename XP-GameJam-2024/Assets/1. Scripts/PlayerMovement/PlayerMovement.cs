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
		public event Action OnMovementStart = delegate { };
		public event Action OnMovementCancelled = delegate { };
		public event Action OnEnterIdle = delegate { };

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

			InputControllerManager.Instance.mainInput.Overworld.Movement.performed += StartMoving;
			InputControllerManager.Instance.mainInput.Overworld.Movement.canceled  += StopMoving;
		}

		private void OnDestroy()
		{
			if (InputControllerManager.IsInitialized)
			{
				InputControllerManager.Instance.mainInput.Overworld.Movement.performed -= StartMoving;
				InputControllerManager.Instance.mainInput.Overworld.Movement.canceled  -= StopMoving;

				// InputControllerManager.Instance.mainInput.Overworld.Interact.performed += OnInteract;
				// InputControllerManager.Instance.mainInput.Overworld.Select.performed += OnSelect;
				// InputControllerManager.Instance.mainInput.Overworld.Start.performed += OnStart;
			}
		}

		private void StartMoving(InputAction.CallbackContext obj)
		{
			isMoving = true;

			Vector2 vector = obj.ReadValue<Vector2>();
			deltaMovement = new Vector3(vector.x, 0, vector.y);

			moveCoroutine ??= StartCoroutine(MovePlayer());
			
			OnMovementStart.Invoke();
		}

		private void StopMoving(InputAction.CallbackContext obj)
		{
			isMoving = false;
			
			OnEnterIdle.Invoke();
			OnMovementCancelled.Invoke();
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