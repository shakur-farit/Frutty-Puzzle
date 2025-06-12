using System.Collections.Generic;
using System.Numerics;
using Entitas;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Code.Gameplay.Features.Grid.Systems
{
	public class GridRhombusLayoutSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _grids;
		private readonly List<GameEntity> _buffer = new(1);

		private readonly IGridLayerCentroid _centroid;

		public GridRhombusLayoutSystem(GameContext game, IGridLayerCentroid centroid)
		{
			_centroid = centroid;
			_grids = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.RhombusLayout,
					GameMatcher.GridColumns,
					GameMatcher.GridRows,
					GameMatcher.GridLayers,
					GameMatcher.CellSizeX,
					GameMatcher.CellSizeY,
					GameMatcher.CellSizeZ)
				.NoneOf(GameMatcher.Available));
		}

		public void Execute()
		{
			foreach (GameEntity grid in _grids.GetEntities(_buffer))
			{
				List<Vector3> positions = GetPositions(
					grid.GridColumns, grid.GridRows, grid.GridLayers, grid.CellSizeX, grid.CellSizeY, grid.CellSizeZ);

				grid.ReplaceCellPositions(positions);

				grid.isAvailable = true;
			}
		}

		private List<Vector3> GetPositions(int columns, int rows, int layers, float sizeX, float sizeY, float sizeZ)
		{
			List<Vector3> result = new();
			List<Vector3> currentLayer = CreateBaseLayer(columns, rows, sizeX, sizeZ, result);

			Vector3 centerOffset = _centroid.GetCentroid(currentLayer);
			CenterLayer(currentLayer, result, centerOffset);

			for (int layer = 1; layer < layers; layer++)
				currentLayer = CreateNextLayer(currentLayer, columns, rows, layer, sizeY, result);

			return result;
		}

		private List<Vector3> CreateBaseLayer(int columns, int rows, float sizeX, float sizeZ, List<Vector3> result)
		{
			List<Vector3> baseLayer = new();

			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < columns; col++)
				{
					float x = (col - row) * sizeX;
					float z = (col + row) * sizeZ;

					Vector3 pos = new Vector3(x, 0f, z);
					baseLayer.Add(pos);
					result.Add(pos);
				}
			}

			return baseLayer;
		}

		private void CenterLayer(List<Vector3> currentLayer, List<Vector3> result, Vector3 centerOffset)
		{
			for (int i = 0; i < currentLayer.Count; i++)
			{
				currentLayer[i] -= centerOffset;
				result[i] = currentLayer[i];
			}
		}

		private List<Vector3> CreateNextLayer(List<Vector3> currentLayer, int columns, 
			int rows, int layer, float sizeY, List<Vector3> result)
		{
			List<Vector3> nextLayer = new();

			int baseCols = columns - (layer - 1);
			int cols = columns - layer;
			int rowsL = rows - layer;

			for (int row = 0; row < rowsL; row++)
			{
				for (int col = 0; col < cols; col++)
				{
					if (AreIndicesValid(currentLayer, row, col, baseCols) == false)
						continue;

					Vector3 center = CalculateCellCenter(currentLayer, row, col, baseCols);
					center.y = layer * sizeY;

					nextLayer.Add(center);
					result.Add(center);
				}
			}

			return nextLayer;
		}

		private bool AreIndicesValid(List<Vector3> currentLayer, int row, int col, int baseCols)
		{
			int i00 = row * baseCols + col;
			int i01 = row * baseCols + (col + 1);
			int i10 = (row + 1) * baseCols + col;
			int i11 = (row + 1) * baseCols + (col + 1);

			return i00 < currentLayer.Count && i01 < currentLayer.Count &&
						 i10 < currentLayer.Count && i11 < currentLayer.Count;
		}

		private Vector3 CalculateCellCenter(List<Vector3> currentLayer, int row, int col, int baseCols)
		{
			int i00 = row * baseCols + col;
			int i01 = row * baseCols + (col + 1);
			int i10 = (row + 1) * baseCols + col;
			int i11 = (row + 1) * baseCols + (col + 1);

			return (currentLayer[i00] + currentLayer[i01] + currentLayer[i10] + currentLayer[i11]) / 4f;
		}
	}
}