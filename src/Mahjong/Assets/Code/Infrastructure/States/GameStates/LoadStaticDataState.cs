using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Cysharp.Threading.Tasks;
using Code.StaticData;

namespace Code.Infrastructure.States.GameStates
{
	public class LoadStaticDataState : SimpleState
	{
		private readonly IStaticDataService _staticDataService;
		private readonly IGameStateMachine _stateMachine;

		public LoadStaticDataState(IStaticDataService staticDataService, IGameStateMachine stateMachine)
		{
			_staticDataService = staticDataService;
			_stateMachine = stateMachine;
		}

		public override async void Enter()
		{
			await LoadStaticData();
			EnterToLoadingHomeScreenState();
		}

		private async UniTask LoadStaticData() =>
			await _staticDataService.Load();

		private void EnterToLoadingHomeScreenState() =>
			_stateMachine.Enter<LoadingHomeScreenState>();
	}
}