using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TileLockController.Systems
{
	public class MarkTileUnlockedOrLockedSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(32);

		private readonly GameContext _game;
		private readonly IGroup<GameEntity> _tiles;
		private readonly IGroup<GameEntity> _lockControllers;

		public MarkTileUnlockedOrLockedSystem(GameContext game)
		{
			_game = game;
			_lockControllers = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.PositionByTile));
		}

		public void Execute()
		{
			foreach (GameEntity controller in _lockControllers)
			foreach (int tileId in controller.PositionByTile.Keys)
			{
				GameEntity tile = _game.GetEntityWithId(tileId);
				tile.isUnlocked = true;
				//Debug.Log($"Tile with {tileId} id on {controller.PositionByTile[tileId]} position is unlocked = {tile.isUnlocked}");
			}
		}
	}
}