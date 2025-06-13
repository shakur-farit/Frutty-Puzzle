using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay
{
	public class TilesCollectorInitializeSystem : IInitializeSystem
	{
		public void Initialize()
		{
			CreateEntity.Empty()
				.AddTargetsBuffer(new List<int>())
				.AddLayerMask(CollisionLayer.Tile.AsMask());
		}
	}
}