using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Data;
using Code.Progress.Provider;

namespace Code.Infrastructure.States.GameStates
{
	public class InitializeProgressState : SimpleState
	{
		private readonly IGameStateMachine _stateMachine;
		private readonly IProgressProvider _progressProvider;

		public InitializeProgressState(IGameStateMachine stateMachine, IProgressProvider progressProvider)
		{
			_stateMachine = stateMachine;
			_progressProvider = progressProvider;
		}

		public override void Enter()
		{
			InitializeProgress();
			EnterToLoadStaticDataState();
		}

		private void InitializeProgress() =>
			CreateNewProgress();

		private void CreateNewProgress() =>
			_progressProvider.SetProgressData(new ProgressData());

		private void EnterToLoadStaticDataState() =>
			_stateMachine.Enter<LoadStaticDataState>();
	}
}