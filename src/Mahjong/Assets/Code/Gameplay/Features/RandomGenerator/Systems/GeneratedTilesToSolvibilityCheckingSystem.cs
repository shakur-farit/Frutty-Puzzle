using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.RandomGenerator.Systems
{
	public class GeneratedTilesToSolvabilityCheckingSystem : IExecuteSystem
	{
		private readonly ISolvabilityChecker _checker;
		private readonly IGroup<GameEntity> _generators;
		private readonly IGroup<GameEntity> _grids;
		private readonly List<GameEntity> _buffer = new(1);

		public GeneratedTilesToSolvabilityCheckingSystem(GameContext game, ISolvabilityChecker checker)
		{
			_checker = checker;
			_generators = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TilePositions)
				.NoneOf(GameMatcher.LevelSolvable));

			_grids = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.CellSizeX,
					GameMatcher.CellSizeY,
					GameMatcher.CellSizeZ));
		}

		public void Execute()
		{
			foreach (GameEntity generators in _generators.GetEntities(_buffer))
			foreach (GameEntity grid in _grids)
			{
				bool isSolvable = _checker.IsSolvable(generators.TilePositions,
					grid.CellSizeX, grid.CellSizeY, grid.CellSizeZ);

				if (isSolvable)
					generators.isLevelSolvable = true;
				else
					generators.isGenerationRequired = true;
			}
		}
	}
}