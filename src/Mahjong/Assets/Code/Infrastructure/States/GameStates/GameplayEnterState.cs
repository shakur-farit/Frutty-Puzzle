using Code.Gameplay.Features.Level;
using Code.Gameplay.Features.Level.Factory;
using Code.Gameplay.Features.Tile;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.UI.Windows;
using Code.Meta.UI.Windows.Service;
using System;
using System.Linq;
using Code.Infrastructure.Random;

namespace Code.Infrastructure.States.GameStates
{
	public class GameplayEnterState : SimpleState
	{
		private readonly IGameStateMachine _stateMachine;
		private readonly ILevelFactory _levelFactory;
		private readonly IWindowService _windowService;
		private readonly IRandomService _random;

		public GameplayEnterState(
			IGameStateMachine stateMachine, 
			ILevelFactory levelFactory,
			IWindowService windowService,
			IRandomService random)
		{
			_stateMachine = stateMachine;
			_levelFactory = levelFactory;
			_windowService = windowService;
			_random = random;
		}

		public override void Enter()
		{
			_windowService.Open(WindowId.Hud);

			LevelId[] levels = GetLevels();
			int randomLevelIndex = _random.Range(0, levels.Length);

			_levelFactory.CreateLevel(levels[randomLevelIndex]);

			EnterToBattleLoop();
		}

		private async void EnterToBattleLoop() =>
			await _stateMachine.Enter<GameplayLoopState>();

		private LevelId[] GetLevels() =>
			Enum.GetValues(typeof(LevelId))
				.Cast<LevelId>()
				.Where(t => t != LevelId.Unknown)
				.ToArray();
	}
}