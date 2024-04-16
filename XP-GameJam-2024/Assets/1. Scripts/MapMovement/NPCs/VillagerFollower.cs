using System;
using System.Collections;
using GameManagement;
using MapMovement.Interfaces;
using UnityEngine;
using VDFramework;

namespace MapMovement.NPCs
{
	[RequireComponent(typeof(NavMeshAgentManager))]
	public class VillagerFollower : BetterMonoBehaviour, IActorMover
	{
		public event Action OnMovementStart = delegate { };
		public event Action OnMovementStopped = delegate { };

		public bool IsFollowing { get; private set; }
		
		public bool WillBeSaved { get; private set; }

		[SerializeField]
		private VillagerController villagerToFollowAtStart;

		// Our components
		private NavMeshAgentManager navMeshAgentManager;
		private Villager villagerComponent;

		// Following Target
		private IActorMover followTarget;
		private Villager followTargetVillager;
		private Coroutine followingTarget;

		private void Awake()
		{
			navMeshAgentManager = GetComponent<NavMeshAgentManager>();
			villagerComponent   = GetComponent<Villager>();

			villagerComponent.OnDeath += OnFollowTargetDeath;
			villagerComponent.OnSave  += OnFollowTargetSaved;

			if (!ReferenceEquals(villagerToFollowAtStart, null))
			{
				followTargetVillager = villagerToFollowAtStart.GetComponent<Villager>();
			}
		}

		private void Start()
		{
			if (ReferenceEquals(followTargetVillager, null))
			{
				return;
			}

			SetFollowTarget(followTargetVillager);
		}

		private void SetFollowTarget(Villager villager)
		{
			if (WillBeSaved) // Don't follow anyone else if we are going to be saved
			{
				return;
			}
			
			followTargetVillager = villager;
			followTarget         = villager.GetComponent<IActorMover>();

			villager.OnDeath += OnFollowTargetDeath;
			villager.OnSave  += OnFollowTargetSaved;

			followTarget.OnMovementStart   += StartMoving;
			followTarget.OnMovementStopped += StopMoving;

			IsFollowing = true;
		}

		private void StopFollowing()
		{
			IsFollowing = false;

			followTarget.OnMovementStart   -= StartMoving;
			followTarget.OnMovementStopped -= StopMoving;
		}

		private void StartMoving()
		{
			followingTarget = StartCoroutine(FollowTarget());

			navMeshAgentManager.Start();
			OnMovementStart.Invoke();
		}

		private void StopMoving()
		{
			StopCoroutine(followingTarget);

			navMeshAgentManager.Stop();
			OnMovementStopped.Invoke();
		}

		private void OnFollowTargetDeath()
		{
			followTargetVillager.OnDeath -= OnFollowTargetDeath;
			followTargetVillager.OnSave  -= OnFollowTargetSaved;
			StopFollowing();
		}

		private void OnFollowTargetSaved()
		{
			// Ignore everything from the follow target and keep going
			followTargetVillager.OnDeath   -= OnFollowTargetDeath;
			followTargetVillager.OnSave    -= OnFollowTargetSaved;
			StopFollowing();

			WillBeSaved = true;
		}

		private IEnumerator FollowTarget()
		{
			do
			{
				navMeshAgentManager.SetDestination(followTargetVillager.CachedTransform.position);
				navMeshAgentManager.Start();
				yield return null;
			} while (IsFollowing);
		}
	}
}