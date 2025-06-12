using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Grid.Systems
{
	public class GridTriangleLayoutSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _grids;
		private readonly List<GameEntity> _buffer = new(1);

		private readonly IGridLayerCentroid _centroid;

		public GridTriangleLayoutSystem(GameContext game, IGridLayerCentroid centroid)
		{
			_centroid = centroid;
			_grids = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TriangleLayout,
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

				TryVisualizeGrid(positions);

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
				int colsInRow = columns - row;
				for (int col = 0; col < colsInRow; col++)
				{
					float x = (col - (colsInRow - 1) * 0.5f) * sizeX;
					float z = row * sizeZ;

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

		private List<Vector3> CreateNextLayer(List<Vector3> currentLayer, int columns, int rows, int layer,
				float sizeY, List<Vector3> result)
		{
			List<Vector3> nextLayer = new();

			int baseCols = columns - (layer - 1);
			int baseRows = rows - (layer - 1);

			for (int row = 0; row < baseRows - 1; row++)
			{
				for (int col = 0; col < baseCols - row - 1; col++)
				{
					if (!AreIndicesValid(currentLayer, row, col, baseCols))
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
			(int i0, int i1, int i2) = GetTriangleIndices(row, col, baseCols);

			return i0 < currentLayer.Count && i1 < currentLayer.Count && i2 < currentLayer.Count;
		}

		private Vector3 CalculateCellCenter(List<Vector3> currentLayer, int row, int col, int baseCols)
		{
			(int i0, int i1, int i2) = GetTriangleIndices(row, col, baseCols);

			return (currentLayer[i0] + currentLayer[i1] + currentLayer[i2]) / 3f;
		}

		private (int i0, int i1, int i2) GetTriangleIndices(int row, int col, int baseCols)
		{
			int baseIndex = GetTriIndex(row, col, baseCols);
			return (baseIndex, baseIndex + 1, baseIndex + baseCols - row);
		}

		private int GetTriIndex(int row, int col, int totalCols)
		{
			int index = 0;
			for (int r = 0; r < row; r++)
			{
				index += totalCols - r;
			}
			return index + col;
		}

		private void TryVisualizeGrid(List<Vector3> positions)
		{
			GameObject visualizerObj = GameObject.Find("Grid Debug");
			if (visualizerObj == null)
				return;

			GridVisualizer visualizer = visualizerObj.GetComponent<GridVisualizer>();
			if (visualizer != null)
				visualizer.SetPositions(positions);
		}
	}
}