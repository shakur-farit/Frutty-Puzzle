using Code.Gameplay.Features.Level;
using Code.Gameplay.Features.Level.Factory;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.UI.Windows;
using Code.Meta.UI.Windows.Service;

namespace Code.Infrastructure.States.GameStates
{
	public class GameplayEnterState : SimpleState
	{
		private readonly IGameStateMachine _stateMachine;
		private readonly ILevelFactory _levelFactory;
		private readonly IWindowService _windowService;

		public GameplayEnterState(
			IGameStateMachine stateMachine, 
			ILevelFactory levelFactory,
			IWindowService windowService)
		{
			_stateMachine = stateMachine;
			_levelFactory = levelFactory;
			_windowService = windowService;
		}

		public override void Enter()
		{
			_windowService.Open(WindowId.Hud);
			_levelFactory.CreateLevel(LevelId.First);

			EnterToBattleLoop();
		}

		private async void EnterToBattleLoop() =>
			await _stateMachine.Enter<GameplayLoopState>();
	}
}