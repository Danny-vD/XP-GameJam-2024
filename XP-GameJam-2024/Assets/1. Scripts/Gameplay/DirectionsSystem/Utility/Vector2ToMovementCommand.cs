using System.Collections.Generic;
using MapMovement.Commands;
using MapMovement.Commands.Interfaces;
using UnityEngine;

namespace Gameplay.DirectionsSystem.Utility
{
	public static class Vector2ToMovementCommand
	{
		private static readonly Dictionary<Vector2, AbstractMoveCommand> commandsPerDirection = new Dictionary<Vector2, AbstractMoveCommand>
		{
			{ Vector2.down, new MoveDownCommand() },
			{ Vector2.up, new MoveUpCommand() },
			{ Vector2.left, new MoveLeftCommand() },
			{ Vector2.right, new MoveRightCommand() },
		};

		public static bool TryGetCommand(Vector2 direction, out AbstractMoveCommand moveCommand)
		{
			return commandsPerDirection.TryGetValue(direction, out moveCommand);
		}

		public static Vector2 ToDirection(AbstractMoveCommand moveCommand)
		{
			return moveCommand switch
			{
				MoveUpCommand => Vector2.up,
				MoveDownCommand => Vector2.down,
				MoveLeftCommand => Vector2.left,
				MoveRightCommand => Vector2.right,
				_ => Vector2.zero,
			};
		}
	}
}