using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.TileLockController.Systems
{
	public interface IRandomTileGenerator
	{
		List<TilePositions> GenerateStructuredTiles(
			int pairCount,
			List<Vector3> allGridSlots,
			float cellSizeY,
			float cellSupportTolerance = 0.1f);
	}
}