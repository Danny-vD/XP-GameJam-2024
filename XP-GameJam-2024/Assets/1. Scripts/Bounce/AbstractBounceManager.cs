using System;
using UnityEngine;
using VDFramework;

namespace PhysicsScripts.Movement.Bouncing
{
	[RequireComponent(typeof(MovementDataHolder))]
	public abstract class AbstractBounceManager : BetterMonoBehaviour
	{
		public event Action OnBounce = delegate { };
		public event Action OnBounceLate = delegate { };
		
		protected MovementDataHolder MovementDataHolder;

		protected virtual void Awake()
		{
			MovementDataHolder = GetComponent<MovementDataHolder>();
		}
		
		protected void RaiseBounceEvent()
		{

			OnBounce.Invoke();
		}
	}
}