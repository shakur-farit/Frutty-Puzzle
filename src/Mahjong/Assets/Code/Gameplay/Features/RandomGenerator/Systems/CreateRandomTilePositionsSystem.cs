using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.RandomGenerator.Systems
{
	public class CreateRandomTilePositionsSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(1);

		private readonly IRandomTilePositionsGenerator _generator;
		private readonly IGroup<GameEntity> _generators;
		private readonly IGroup<GameEntity> _levels;
		private readonly IGroup<GameEntity> _grids;

		public CreateRandomTilePositionsSystem(GameContext game, IRandomTilePositionsGenerator generator)
		{
			_generator = generator;
			_generators = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TilePositions,
					GameMatcher.GenerationRequired));

			_levels = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TilePairsOnLevel));

			_grids = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.CellPositions));
		}

		public void Execute()
		{
			foreach (GameEntity generators in _generators.GetEntities(_buffer))
			foreach (GameEntity level in _levels)
			foreach (GameEntity grid in _grids)
			{
				List<TilePosition> tilePositions = _generator
					.GenerateRandomTilePositions(level.TilePairsOnLevel, grid.CellPositions);

				generators.ReplaceTilePositions(tilePositions);
				generators.isGenerationRequired = false;
			}
		}
	}
}

	