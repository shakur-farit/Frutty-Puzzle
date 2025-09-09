using Code.Common.Destruct;
using Code.Gameplay.Features.Grid;
using Code.Gameplay.Features.Level;
using Code.Gameplay.Features.Movement;
using Code.Gameplay.Features.RandomGenerator;
using Code.Gameplay.Features.RandomGenerator.Systems;
using Code.Gameplay.Features.TargetsCollection;
using Code.Gameplay.Features.Tile;
using Code.Gameplay.Features.TileComparer;
using Code.Gameplay.Features.TileLockController;
using Code.Gameplay.Input;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay
{
	public sealed class BattleFeature : Feature
	{
		public BattleFeature(ISystemsFactory systems)
		{
			Add(systems.Create<InputFeature>());
			Add(systems.Create<LevelFeature>());
			Add(systems.Create<GridFeature>());
			Add(systems.Create<RandomTilePositionsGeneratorFeature>());
			Add(systems.Create<TileFeature>());
			Add(systems.Create<BindViewFeature>());
			Add(systems.Create<TileLockControllerFeature>());
			Add(systems.Create<TargetsCollectionFeature>());
			Add(systems.Create<TileComparerFeature>());
			Add(systems.Create<MovementFeature>());

			Add(systems.Create<ProcessGameDestructedFeature>());
		}
	}
}