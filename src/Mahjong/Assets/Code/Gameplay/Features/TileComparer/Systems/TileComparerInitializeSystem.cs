using Code.Common.Entity;
using Entitas;

namespace Code.Gameplay.Features.TileComparer.Systems
{
	public class TileComparerInitializeSystem : IInitializeSystem
	{
		public void Initialize()
		{
			CreateEntity.Empty()
				.AddTileCompareList(new())
				.AddCompareListLimit(2)
				;
		}
	}
}