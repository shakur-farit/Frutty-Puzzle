using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TileComparer.Systems
{
	public class MarkDestructSameTargetsSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(32);
		private readonly IGroup<GameEntity> _targets;
		private readonly IGroup<GameEntity> _levels;

		public MarkDestructSameTargetsSystem(GameContext game)
		{
			_targets = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Tile,
					GameMatcher.Same));

			_levels = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.CurrentTilesCountOnLevel));
		}

		public void Execute()
		{
			foreach (GameEntity target in _targets.GetEntities(_buffer))
			foreach (GameEntity level in _levels)
			{
				level.ReplaceCurrentTilesCountOnLevel(level.CurrentTilesCountOnLevel - 1);
				Debug.Log(level.CurrentTilesCountOnLevel);
				target.isProcessedTarget = true;
				target.isDestructed = true;
			}
		}
	}
}