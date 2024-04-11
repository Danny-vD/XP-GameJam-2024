using System.Collections.Generic;
using Gameplay.DirectionsSystem.NPCs;
using Gameplay.InteractSystem.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using VDFramework;

namespace Gameplay.DirectionsSystem.Player
{
	/*
	 * Listen to player interact, filter out the villagers
	 *
	 * Use special movement to set directions
	 * sent to the villagers upon receiving special interact
	 */
	public class DirectionGiver : BetterMonoBehaviour
	{
		[SerializeField]
		private InputActionReference specialMovementInput;

		[SerializeField]
		private InputActionReference specialInteractInput;
		
		private List<DirectionsReceiver> directionsReceivers;
		
		private PlayerInteract playerInteract;

		private void Awake()
		{
			playerInteract = GetComponent<PlayerInteract>();
		}

		private void OnEnable()
		{
			playerInteract.OnInteractWithDirectionReceivers += SetDirectionReceiver;
		}

		private void SetDirectionReceiver(List<DirectionsReceiver> directionReceivers)
		{
			this.directionsReceivers = directionReceivers;
		}
	}
}