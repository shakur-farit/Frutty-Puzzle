using Code.Common.Entity;
using Entitas;

namespace Code.Gameplay.Features.TileComparer
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