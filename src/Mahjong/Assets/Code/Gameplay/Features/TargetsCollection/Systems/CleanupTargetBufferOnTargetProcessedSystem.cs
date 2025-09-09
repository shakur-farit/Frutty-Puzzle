using Entitas;

namespace Code.Gameplay.Features.TargetsCollection.Systems
{
	public class CleanupTargetBufferOnTargetProcessedSystem : ICleanupSystem
	{
		private readonly IGroup<GameEntity> _collectors;
		private readonly IGroup<GameEntity> _targets;

		public CleanupTargetBufferOnTargetProcessedSystem(GameContext game)
		{
			_collectors = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TargetsBuffer));

			_targets = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.ProcessedTarget));
		}

		public void Cleanup()
		{
			foreach (GameEntity collector in _collectors)
			foreach (GameEntity target in _targets)
			{
				collector.TargetsBuffer.Remove(target.Id);
			}
		}
	}
}