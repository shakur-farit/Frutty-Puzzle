using Code.Gameplay;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.Systems;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
	public class GameplayLoopState : EndOfFrameExitState
	{
		private BattleFeature _battleFeature;

		private readonly ISystemsFactory _systemsFactory;
		private readonly GameContext _gameContext;

		public GameplayLoopState(ISystemsFactory systemsFactory, GameContext gameContext)
		{
			_systemsFactory = systemsFactory;
			_gameContext = gameContext;
		}

		public override void Enter()
		{
			_battleFeature = _systemsFactory.Create<BattleFeature>();

			_battleFeature.Initialize();
		}

		protected override void OnUpdate()
		{
			_battleFeature.Execute();
			_battleFeature.Cleanup();
		}

		protected override void ExitOnEndOfFrame()
		{
			_battleFeature.DeactivateReactiveSystems();
			_battleFeature.ClearReactiveSystems();

			DestructEntities();

			_battleFeature.Cleanup();

			_battleFeature.TearDown();
			_battleFeature = null;
		}

		private void DestructEntities()
		{
			foreach (GameEntity entity in _gameContext.GetEntities())
				entity.isDestructed = true;
		}
	}
}