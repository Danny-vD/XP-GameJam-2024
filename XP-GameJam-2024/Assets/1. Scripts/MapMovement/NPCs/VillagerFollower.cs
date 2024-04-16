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
			navMeshAgentManager       =  GetComponent<NavMeshAgentManager>();
			villagerComponent         =  GetComponent<Villager>();
			
			villagerComponent.OnDeath += OnFollowTargetDeath;

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
			followTargetVillager = villager;
			followTarget         = villager.GetComponent<IActorMover>();

			villager.OnDeath += OnFollowTargetDeath;

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
			StopFollowing();
		}

		private IEnumerator FollowTarget()
		{
			do
			{
				navMeshAgentManager.SetDestination(followTargetVillager.CachedTransform.position);
				yield return null;
			} while (IsFollowing);
		}
	}
}