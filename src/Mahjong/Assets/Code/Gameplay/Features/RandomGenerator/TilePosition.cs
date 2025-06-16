using Code.Gameplay.Features.Tile;
using UnityEngine;

namespace Code.Gameplay.Features.RandomGenerator
{
	public struct TilePosition
	{
		public TileTypeId Id;
		public Vector3 Position;

		public static TilePosition Set(TileTypeId id, Vector3 position)
		{
			return new TilePosition()
			{
				Id = id,
				Position = position
			};
		}
	}
}