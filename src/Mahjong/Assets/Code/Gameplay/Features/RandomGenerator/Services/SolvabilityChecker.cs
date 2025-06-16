using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Tile;
using Code.Gameplay.Features.TileLockController.Services;
using UnityEngine;

namespace Code.Gameplay.Features.RandomGenerator.Systems
{
	public class SolvabilityChecker : ISolvabilityChecker
	{
		private readonly ITileLockOrUnlockChecker _lockChecker;

		public SolvabilityChecker(ITileLockOrUnlockChecker lockChecker) => 
			_lockChecker = lockChecker;

		public bool IsSolvable(List<TilePosition> tiles,
			float sizeX, float sizeY, float sizeZ)
		{
			List<TilePosition> remainingTiles = new List<TilePosition>(tiles);
			List<Vector3> remainingPositions = remainingTiles.Select(t => t.Position).ToList();

			while (true)
			{
				List<TilePosition> unlocked = new List<TilePosition>();

				for (int i = 0; i < remainingTiles.Count; i++)
				{
					if (_lockChecker.IsLocked(remainingTiles[i].Position, remainingPositions, sizeX, sizeY, sizeZ) == false)
						unlocked.Add(remainingTiles[i]);
				}

				List<IGrouping<TileTypeId, TilePosition>> grouped = unlocked
					.GroupBy(t => t.Id)
					.Where(g => g.Count() >= 2)
					.ToList();

				if (grouped.Count == 0)
					break;

				foreach (IGrouping<TileTypeId, TilePosition> group in grouped)
				{
					TilePosition first = group.ElementAt(0);
					TilePosition second = group.ElementAt(1);

					remainingTiles.Remove(first);
					remainingTiles.Remove(second);

					remainingPositions.Remove(first.Position);
					remainingPositions.Remove(second.Position);
				}
			}

			return remainingTiles.Count == 0;
		}
	}
}