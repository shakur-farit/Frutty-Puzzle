using Code.Gameplay.Features.Grid.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Grid
{
	public sealed class GridFeature : Feature
	{
		public GridFeature(ISystemsFactory systems)
		{
			Add(systems.Create<GridSquareLayoutSystem>());
		}
	}
}