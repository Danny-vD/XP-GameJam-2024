using System.Collections.Generic;
using MapMovement.Commands.Interface;
using VDFramework;

namespace Gameplay.DirectionsSystem.NPCs
{
	public class DirectionsReceiver : BetterMonoBehaviour
	{
		public bool CanReceiveDirections { get; private set; } = true; // TODO Check if the movement queue in DirectionsHolder contains commands
		
		public void SetDirections(Queue<AbstractMoveCommand> directions)
		{
			CanReceiveDirections = false;
		}
	}
}