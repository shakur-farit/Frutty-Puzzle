using System.Collections.Generic;

namespace Code.Gameplay.Features.RandomGenerator.Systems
{
	public interface ISolvabilityChecker
	{
		bool IsSolvable(List<TilePosition> tiles,
			float sizeX, float sizeY, float sizeZ);
	}
}