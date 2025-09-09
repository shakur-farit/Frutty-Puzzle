using UnityEngine;

namespace Code.Gameplay.Features.Tile.Factory
{
	public interface ITileFactory
	{
		GameEntity CreateTile(TileTypeId typeId, Vector3 at,
			float tileSizeX, float tileSizeY, float tileSizeZ);
	}
}