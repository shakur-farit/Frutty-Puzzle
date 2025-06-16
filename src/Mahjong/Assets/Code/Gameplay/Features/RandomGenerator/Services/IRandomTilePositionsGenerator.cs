using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.RandomGenerator
{
	public interface IRandomTilePositionsGenerator
	{
		List<TilePosition> GenerateRandomTilePositions(int pairCount, List<Vector3> allGridSlots, 
			float cellSupportTolerance = 0.1f);
	}
}