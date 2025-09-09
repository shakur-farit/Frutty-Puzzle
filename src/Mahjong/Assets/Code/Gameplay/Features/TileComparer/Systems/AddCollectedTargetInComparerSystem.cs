using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TileComparer.Systems
{
	public class AddCollectedTargetInComparerSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(1);

		private readonly IGroup<GameEntity> _targets;
		private readonly IGroup<GameEntity> _comparers;

		public AddCollectedTargetInComparerSystem(GameContext game)
		{
			_targets = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.CollectedTarget,
					GameMatcher.Unlocked)
				.NoneOf(GameMatcher.Selected));

			_comparers = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TileCompareList));
		}

		public void Execute()
		{
			foreach (GameEntity comparer in _comparers)
			foreach (GameEntity target in _targets.GetEntities(_buffer))
				comparer.TileCompareList.Add(target.Id);
		}
	}
}