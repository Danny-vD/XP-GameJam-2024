using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using VDFramework;

namespace MapMovement
{
	public class PlayerMovement : BetterMonoBehaviour, IActorMover
	{
		public event Action OnMovementStart = delegate { };
		public event Action OnEnterIdle = delegate { };

		[SerializeField]
		[Tooltip("The speed of the player in m/s")]
		private float speed = 10;

		[SerializeField]
		private InputActionReference movement;

		private CharacterController agent;
		private Vector3 deltaMovement;
		private bool isMoving;

		private Coroutine moveCoroutine = null;

		private void Awake()
		{
			agent = GetComponent<CharacterController>();

			// Movement
			movement.action.performed += StartMoving;
			movement.action.canceled  += StopMoving;
		}

		private void OnDestroy()
		{
			// Movement
			movement.action.performed -= StartMoving;
			movement.action.canceled  -= StopMoving;
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