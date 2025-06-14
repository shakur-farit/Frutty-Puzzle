using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.TileComparer
{
	public sealed class TileComparerFeature : Feature
	{
		public TileComparerFeature(ISystemsFactory systems)
		{
			Add(systems.Create<TileComparerInitializeSystem>());

			Add(systems.Create<MarkCompareListFullSystem>());
			Add(systems.Create<TileCompareSystem>());
			Add(systems.Create<MarkDestructSameTilesSystem>());
		}
	}
}