using Gameplay.InteractSystem.Player;
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
		private PlayerInteract playerInteract;
	}
}