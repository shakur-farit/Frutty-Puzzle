using Code.Gameplay.Common.Collisions;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Grid.Factory;
using Code.Gameplay.Features.Grid.Services;
using Code.Gameplay.Features.Grid.Systems;
using Code.Gameplay.Features.Level.Factory;
using Code.Gameplay.Features.RandomGenerator;
using Code.Gameplay.Features.RandomGenerator.Systems;
using Code.Gameplay.Features.Tile.Factory;
using Code.Gameplay.Features.TileLockController.Services;
using Code.Gameplay.Features.TileLockController.Systems;
using Code.Gameplay.Input.Services;
using Code.Infrastructure.AsstesManagement;
using Code.Infrastructure.Identifiers;
using Code.Infrastructure.Loading;
using Code.Infrastructure.Random;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View.Factory;
using Code.Meta.UI.Windows.Factory;
using Code.Meta.UI.Windows.Service;
using Code.Progress.Provider;
using Code.StaticData;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
	{
		public override void InstallBindings()
		{
			BindStateMachine();
			BindStateFactory();
			BindGameStates();
			BindSystemFactory();
			BindInfrastructureServices();
			BindAssetManagementServices();
			BindCommonServices();
			BindContexts();
			BindGameplayServices();
			BindGameplayFactories();
			BindUIServices();
			BindUIFactories();
			BindCameraProvider();
			BindProgressServices();
			BindStaticDataServices();
		}

		private void BindStateMachine()
		{
			Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
		}

		private void BindStateFactory()
		{
			Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
		}

		private void BindGameStates()
		{
			Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
			Container.BindInterfacesAndSelfTo<InitializeProgressState>().AsSingle();
			Container.BindInterfacesAndSelfTo<LoadStaticDataState>().AsSingle();
			Container.BindInterfacesAndSelfTo<LoadingHomeScreenState>().AsSingle();
			Container.BindInterfacesAndSelfTo<HomeScreenEnterState>().AsSingle();
			Container.BindInterfacesAndSelfTo<HomeScreenState>().AsSingle();
			Container.BindInterfacesAndSelfTo<LoadingGameplayState>().AsSingle();
			Container.BindInterfacesAndSelfTo<GameplayEnterState>().AsSingle();
			Container.BindInterfacesAndSelfTo<GameplayLoopState>().AsSingle();
		}

		private void BindContexts()
		{
			Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();

			Container.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
		}

		private void BindCameraProvider()
		{
			//Container.BindInterfacesAndSelfTo<CameraProvider>().AsSingle();
		}

		private void BindProgressServices()
		{
			Container.Bind<IProgressProvider>().To<ProgressProvider>().AsSingle();
		}

		private void BindStaticDataServices()
		{
			Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
		}

		private void BindGameplayServices()
		{
			Container.Bind<IGridLayerCentroid>().To<GridLayerCentroid>().AsSingle();
			Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
			Container.Bind<ITileLockOrUnlockChecker>().To<TileLockOrUnlockChecker>().AsSingle();
			Container.Bind<IRandomTilePositionsGenerator>().To<RandomTilePositionsGenerator>().AsSingle();
			Container.Bind<ISolvabilityChecker>().To<SolvabilityChecker>().AsSingle();
		}

		private void BindGameplayFactories()
		{
			Container.Bind<IEntityViewFactory>().To<EntityViewFactory>().AsSingle();
			Container.Bind<ITileFactory>().To<TileFactory>().AsSingle();
			Container.Bind<IGridFactory>().To<GridFactory>().AsSingle();
			Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
		}

		private void BindUIServices()
		{
			Container.Bind<IWindowService>().To<WindowService>().AsSingle();
		}

		private void BindUIFactories()
		{
			Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
		}

		private void BindSystemFactory()
		{
			Container.Bind<ISystemsFactory>().To<SystemsFactory>().AsSingle();
		}

		private void BindInfrastructureServices()
		{
			Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
			Container.Bind<IIdentifierService>().To<IdentifierService>().AsSingle();
		}

		private void BindAssetManagementServices()
		{
			Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
		}

		private void BindCommonServices()
		{
			Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
			Container.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle();
			Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
			Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
			Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
		}

		public void Initialize()
		{
			Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
		}
	}
}
