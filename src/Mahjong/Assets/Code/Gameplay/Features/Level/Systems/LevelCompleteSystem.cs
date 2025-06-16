using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Entitas;

namespace Code.Gameplay.Features.Level.Systems
{
	public class LevelCompleteSystem : IExecuteSystem
	{
		private readonly IGameStateMachine _stateMachine;
		private readonly IGroup<GameEntity> _levels;

		public LevelCompleteSystem(GameContext game, IGameStateMachine stateMachine)
		{
			_stateMachine = stateMachine;
			_levels = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.CurrentTilesCountOnLevel));
		}

		public void Execute()
		{
			foreach (GameEntity level in _levels)
			{
				if (level.CurrentTilesCountOnLevel <= 0)
					_stateMachine.Enter<GameplayEnterState>();
			}
		}
	}
}