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