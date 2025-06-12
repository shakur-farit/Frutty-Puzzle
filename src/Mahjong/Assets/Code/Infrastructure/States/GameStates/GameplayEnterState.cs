using Code.Gameplay.Features.Level;
using Code.Gameplay.Features.Level.Factory;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
	public class GameplayEnterState : SimpleState
	{
		private readonly IGameStateMachine _stateMachine;
		private readonly ILevelFactory _levelFactory;

		public GameplayEnterState(IGameStateMachine stateMachine, ILevelFactory levelFactory)
		{
			_stateMachine = stateMachine;
			_levelFactory = levelFactory;
		}

		public override void Enter()
		{
			_levelFactory.CreateLevel(LevelId.Second);

			EnterToBattleLoop();
		}

		private void EnterToBattleLoop() =>
			_stateMachine.Enter<GameplayLoopState>();
	}
}