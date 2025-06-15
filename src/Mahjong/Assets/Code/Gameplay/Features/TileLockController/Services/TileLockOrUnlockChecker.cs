using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.TileLockController.Systems
{
	public class TileLockOrUnlockChecker : ITileLockOrUnlockChecker
	{
		public bool IsLocked(Vector3 tilePosition, List<Vector3> positions, float sizeX,
			float sizeY, float sizeZ, float tolerance = 0.01f, float offsetTolerance = 0.71f)
		{
			bool hasLeft = false;
			bool hasRight = false;
			bool hasTop = false;

			foreach (Vector3 otherTilesPosition in positions)
			{
				if (Mathf.Abs(otherTilesPosition.y - tilePosition.y) < tolerance)
				{
					if (Mathf.Abs(otherTilesPosition.x - (tilePosition.x - sizeX)) < tolerance &&
					    Mathf.Abs(otherTilesPosition.z - tilePosition.z) < tolerance)
						hasLeft = true;

					if (Mathf.Abs(otherTilesPosition.x - (tilePosition.x + sizeX)) < tolerance &&
					    Mathf.Abs(otherTilesPosition.z - tilePosition.z) < tolerance)
						hasRight = true;
				}

				bool closeToTopY = Mathf.Abs(otherTilesPosition.y - (tilePosition.y + sizeY)) < tolerance;
				bool closeToTopX = Mathf.Abs(otherTilesPosition.x - tilePosition.x) <= sizeX * offsetTolerance;
				bool closeToTopZ = Mathf.Abs(otherTilesPosition.z - tilePosition.z) <= sizeZ * offsetTolerance;

				if (closeToTopY && closeToTopX && closeToTopZ)
					hasTop = true;

				if ((hasLeft && hasRight) || hasTop)
					return true;
			}

			return false;
		}
	}
}