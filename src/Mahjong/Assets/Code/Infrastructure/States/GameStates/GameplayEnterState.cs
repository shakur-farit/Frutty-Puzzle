using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
	public class GameplayEnterState : SimpleState
	{
		private readonly IGameStateMachine _stateMachine;

		public GameplayEnterState(IGameStateMachine stateMachine) => 
			_stateMachine = stateMachine;

		public override void Enter()
		{
			EnterToBattleLoop();
		}

		private void EnterToBattleLoop() =>
			_stateMachine.Enter<GameplayLoopState>();
	}
}