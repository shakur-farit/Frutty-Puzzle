using Code.Gameplay.Features.Grid;
using Code.Gameplay.Features.Grid.Factory;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
	public class GameplayEnterState : SimpleState
	{
		private readonly IGameStateMachine _stateMachine;
		private readonly IGridFactory _gridFactory;

		public GameplayEnterState(IGameStateMachine stateMachine, IGridFactory gridFactory)
		{
			_stateMachine = stateMachine;
			_gridFactory = gridFactory;
		}

		public override void Enter()
		{
			_gridFactory.CreateGrid(GridTypeId.XSquare);

			EnterToBattleLoop();
		}

		private void EnterToBattleLoop() =>
			_stateMachine.Enter<GameplayLoopState>();
	}
}