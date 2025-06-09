using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Cysharp.Threading.Tasks;
using Code.StaticData;
using Code.Infrastructure.Loading;
using Code.Progress.Provider;

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

	public class LoadingGameplayState : SimplePayloadState<string>
	{
		private readonly IGameStateMachine _stateMachine;
		private readonly ISceneLoader _sceneLoader;

		public LoadingGameplayState(IGameStateMachine stateMachine, ISceneLoader sceneLoader)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
		}

		public override void Enter(string sceneName) =>
			LoadGameplayScene(sceneName);

		private void LoadGameplayScene(string sceneName) =>
			_sceneLoader.LoadScene(sceneName, EnterBattleLoopState);

		private void EnterBattleLoopState() =>
			_stateMachine.Enter<GameplayEnterState>();
	}
}