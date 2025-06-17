using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.UI.Windows;
using Code.Meta.UI.Windows.Service;
using Entitas;

namespace Code.Gameplay.Features.Level.Systems
{
	public class LevelCompleteSystem : IExecuteSystem
	{
		private readonly IGameStateMachine _stateMachine;
		private readonly IWindowService _windowService;
		private readonly IGroup<GameEntity> _levels;

		public LevelCompleteSystem(
			GameContext game, 
			IGameStateMachine stateMachine,
			IWindowService windowService)
		{
			_stateMachine = stateMachine;
			_windowService = windowService;
			_levels = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.CurrentTilesCountOnLevel));
		}

		public void Execute()
		{
			foreach (GameEntity level in _levels)
			{
				if (level.CurrentTilesCountOnLevel <= 0)
				{
					_windowService.Close(WindowId.Hud);
					_stateMachine.Enter<GameplayEnterState>();
				}
			}
		}
	}
}