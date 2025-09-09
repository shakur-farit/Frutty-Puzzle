using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
	public class LoadingHomeScreenState : SimpleState
	{
		private readonly IGameStateMachine _stateMachine;
		private readonly ISceneLoader _sceneLoader;

		public LoadingHomeScreenState(IGameStateMachine stateMachine, ISceneLoader sceneLoader)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
		}

		public override void Enter() =>
			LoadHomeScreenScene();

		private void LoadHomeScreenScene() =>
			_sceneLoader.LoadScene(Scenes.HomeScreen, EnterHomeScreenState);

		private void EnterHomeScreenState() =>
			_stateMachine.Enter<HomeScreenEnterState>();
	}
}