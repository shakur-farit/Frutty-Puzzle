using Entitas;

namespace Code.Gameplay.Features.TargetsCollection.Systems
{
	public class RemoveProcessedTargetFromTargetBufferSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _targets;
		private readonly IGroup<GameEntity> _collectors;

		public RemoveProcessedTargetFromTargetBufferSystem(GameContext game)
		{
			_targets = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.ProcessedTarget));

			_collectors = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TargetsBuffer));
		}

		public void Execute()
		{
			foreach (GameEntity target in _targets)
			foreach (GameEntity collector in _collectors)
				collector.TargetsBuffer.Remove(target.Id);
		}
	}
}