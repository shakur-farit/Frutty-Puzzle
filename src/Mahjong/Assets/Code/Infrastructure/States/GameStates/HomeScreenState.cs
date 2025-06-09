using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameStates
{
	public class HomeScreenState : EndOfFrameExitState
	{
		//private HomeScreenFeature _homeScreenFeature;

		//private readonly ISystemsFactory _systems;
		//private readonly GameContext _gameContext;

		//public HomeScreenState(
		//	ISystemsFactory systems,
		//	GameContext gameContext)
		//{
		//	_systems = systems;
		//	_gameContext = gameContext;
		//}

		//public override void Enter()
		//{
		//	_homeScreenFeature = _systems.Create<HomeScreenFeature>();
		//	_homeScreenFeature.Initialize();
		//}

		//protected override void OnUpdate()
		//{
		//	_homeScreenFeature.Execute();
		//	_homeScreenFeature.Cleanup();
		//}

		//protected override void ExitOnEndOfFrame()
		//{
		//	_homeScreenFeature.DeactivateReactiveSystems();
		//	_homeScreenFeature.ClearReactiveSystems();

		//	DestructEntities();

		//	_homeScreenFeature.Cleanup();
		//	_homeScreenFeature.TearDown();
		//	_homeScreenFeature = null;
		//}

		//private void DestructEntities()
		//{
		//	foreach (GameEntity entity in _gameContext.GetEntities())
		//		entity.isDestructed = true;
		//}
	}
}