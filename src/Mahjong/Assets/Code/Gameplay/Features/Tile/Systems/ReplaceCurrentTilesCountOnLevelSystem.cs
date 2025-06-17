using Entitas;

namespace Code.Gameplay.Features.Tile.Systems
{
	public class ReplaceCurrentTilesCountOnLevelSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _levels;
		private readonly IGroup<GameEntity> _controllers;

		public ReplaceCurrentTilesCountOnLevelSystem(GameContext game)
		{
			_levels = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.LevelAvailable));

			_controllers = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.PositionByTile));
		}

		public void Execute()
		{
			foreach (GameEntity level in _levels)
			foreach (GameEntity controller in _controllers)
				level.ReplaceCurrentTilesCountOnLevel(controller.PositionByTile.Count);
		}
	}
}