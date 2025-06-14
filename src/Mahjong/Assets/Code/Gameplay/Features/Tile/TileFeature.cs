using Code.Gameplay.Features.Tile.Systems;
using Code.Gameplay.Features.TileLockController.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Tile
{
	public sealed class TileFeature : Feature
	{
		public TileFeature(ISystemsFactory systems)
		{
			Add(systems.Create<TilesCollectorInitializeSystem>());

			Add(systems.Create<CreateTileSystem>());
			Add(systems.Create<SelectUnlockedTilesOnCLickSystem>());
		}
	}
}