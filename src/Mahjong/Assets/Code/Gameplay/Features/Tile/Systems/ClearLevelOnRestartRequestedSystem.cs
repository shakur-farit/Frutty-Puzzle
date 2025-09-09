using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Tile.Systems
{
	public class ClearLevelOnRestartRequestedSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _tilesBuffer = new(64);
		private readonly List<GameEntity> _generatorsBuffer = new(1);

		private readonly IGroup<GameEntity> _requests;
		private readonly IGroup<GameEntity> _tiles;
		private readonly IGroup<GameEntity> _generators;

		public ClearLevelOnRestartRequestedSystem(GameContext game)
		{
			_requests = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.RestartRequested));

			_tiles = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Tile));

			_generators = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TilesCreated));
		}

		public void Execute()
		{
			foreach (GameEntity request in _requests)
			foreach (GameEntity generator in _generators.GetEntities(_generatorsBuffer))
			{
				foreach (GameEntity tile in _tiles.GetEntities(_tilesBuffer)) 
					tile.isDestructed = true;

				generator.isTilesCreated = false;
				request.isDestructed = true;
			}
		}
	}
}