using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using VDFramework;

namespace Animations
{
	[RequireComponent(typeof(Animator), typeof(IActorMover))]
	public class MovementAnimationManager : BetterMonoBehaviour
	{
		private static readonly int walking = Animator.StringToHash("Walking");
		
		private Animator animator;

		private IActorMover actorMover;

		private void Awake()
		{
			animator   = GetComponent<Animator>();
			actorMover = GetComponent<IActorMover>();
		}

		private void OnEnable()
		{
			actorMover.OnMovementStarted += StartMoving;
			actorMover.IdleEntered += StopMoving;
		}

		private void OnDisable()
		{
			actorMover.OnMovementStarted -= StartMoving;
			actorMover.IdleEntered       -= StopMoving;
		}

		private void StartMoving()
		{
			animator.SetBool(walking, true);
		}
		
		private void StopMoving()
		{
			animator.SetBool(walking, false);
		}
	}
}