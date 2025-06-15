using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.TileLockController.Systems
{
	public interface ITileLockOrUnlockChecker
	{
		bool IsLocked(Vector3 tilePosition, List<Vector3> positions, float sizeX,
			float sizeY, float sizeZ, float tolerance = 0.01f, float offsetTolerance = 0.71f);
	}
}