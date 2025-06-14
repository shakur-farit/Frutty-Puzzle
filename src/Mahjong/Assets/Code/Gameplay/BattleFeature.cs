using Code.Common.Destruct;
using Code.Gameplay.Features.Grid;
using Code.Gameplay.Features.Level;
using Code.Gameplay.Features.Movement;
using Code.Gameplay.Features.TargetsCollection;
using Code.Gameplay.Features.Tile;
using Code.Gameplay.Features.TileComparer;
using Code.Gameplay.Features.TileLockController;
using Code.Gameplay.Input;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View;

namespace Code.Gameplay
{
	public sealed class BattleFeature : Feature
	{
		public BattleFeature(ISystemsFactory systems)
		{
			Add(systems.Create<LevelFeature>());
			Add(systems.Create<GridFeature>());
			Add(systems.Create<TileLockControllerFeature>());
			Add(systems.Create<TileFeature>());
			Add(systems.Create<BindViewFeature>());
			Add(systems.Create<InputFeature>());
			Add(systems.Create<TargetsCollectionFeature>());
			Add(systems.Create<TileComparerFeature>());
			Add(systems.Create<MovementFeature>());

			Add(systems.Create<ProcessGameDestructedFeature>());
		}
	}
}