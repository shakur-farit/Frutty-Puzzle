using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.TileComparer.Systems
{
	public class MarkDestructSameTargetsSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(32);
		private readonly IGroup<GameEntity> _targets;

		public MarkDestructSameTargetsSystem(GameContext game)
		{
			_targets = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Tile,
					GameMatcher.Same));
		}

		public void Execute()
		{
			foreach (GameEntity target in _targets.GetEntities(_buffer))
			{
				target.isProcessedTarget = true;
				target.isDestructed = true;
			}
		}
	}
}