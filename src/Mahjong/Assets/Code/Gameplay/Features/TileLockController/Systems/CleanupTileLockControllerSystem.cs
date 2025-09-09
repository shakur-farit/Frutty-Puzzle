using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.TileLockController.Systems
{
	public class CleanupTileLockControllerSystem : ICleanupSystem
	{
		private readonly List<GameEntity> _buffer = new(1);

		private readonly IGroup<GameEntity> _controllers;
		private readonly IGroup<GameEntity> _tiles;

		public CleanupTileLockControllerSystem(GameContext game)
		{
			_controllers = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.PositionByTile));

			_tiles = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Id,
					GameMatcher.ProcessedTarget));
		}

		public void Cleanup()
		{
			foreach (GameEntity controller in _controllers.GetEntities(_buffer))
			foreach (GameEntity tile in _tiles)
			{
				controller.PositionByTile.Remove(tile.Id);
			}
		}
	}
}