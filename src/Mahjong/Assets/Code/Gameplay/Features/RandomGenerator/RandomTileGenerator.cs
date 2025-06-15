using System.Collections.Generic;
using System;
using System.Linq;
using Code.Gameplay.Features.Tile;
using Code.Infrastructure.Random;
using UnityEngine;

namespace Code.Gameplay.Features.TileLockController.Systems
{
	public class RandomTileGenerator : IRandomTileGenerator
	{
		private readonly IRandomService _random;

		public RandomTileGenerator(IRandomService random) => 
			_random = random;

		public List<TilePositions> GenerateStructuredTiles(int pairCount, List<Vector3> allGridSlots, float cellSizeY,
			 float cellSupportTolerance = 0.1f)
		{
			TileTypeId[] availableTypes = GetAvailableTypes();

			int totalTiles = pairCount * 2;

			if (totalTiles == 0 || allGridSlots.Count == 0)
				return new List<TilePositions>();

			int usableTileCount = Mathf.Min(totalTiles, allGridSlots.Count);
			List<Vector3> selectedPositions = allGridSlots.Take(usableTileCount).ToList();

			List<TileTypeId> tilePool = new List<TileTypeId>();
			while (tilePool.Count < usableTileCount)
			{
				TileTypeId t = availableTypes[_random.Range(0, availableTypes.Length)];
				tilePool.Add(t);
				tilePool.Add(t);
			}

			tilePool = tilePool.Take(usableTileCount).ToList();

			Shuffle(tilePool);
			Shuffle(selectedPositions);

			List<TilePositions> result = new List<TilePositions>();

			for (int i = 0; i < usableTileCount; i++) 
				result.Add(TilePositions.Set(tilePool[i], selectedPositions[i]));

			return result;
		}

		private TileTypeId[] GetAvailableTypes()
		{
			return Enum.GetValues(typeof(TileTypeId))
				.Cast<TileTypeId>()
				.Where(t => t != TileTypeId.Unknown)
				.ToArray();
		}

		private void Shuffle<T>(List<T> list)
		{
			for (int i = list.Count - 1; i > 0; i--)
			{
				int j = _random.Range(0, i + 1);
				(list[i], list[j]) = (list[j], list[i]);
			}
		}
	}

	public struct TilePositions
	{
		public TileTypeId Id;
		public Vector3 Position;

		public static TilePositions Set(TileTypeId id, Vector3 position)
		{
			return new TilePositions()
			{
				Id = id,
				Position = position
			};
		}
	}
}