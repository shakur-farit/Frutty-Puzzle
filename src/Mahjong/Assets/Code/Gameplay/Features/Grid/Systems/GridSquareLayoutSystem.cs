using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Grid.Systems
{
	public class GridSquareLayoutSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _grids;
		private readonly List<GameEntity> _buffer = new(1);

		public GridSquareLayoutSystem(GameContext game)
		{
			_grids = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.SquareLayout)
				.NoneOf(GameMatcher.Available));
		}

		public void Execute()
		{
			foreach (GameEntity grid in _grids.GetEntities(_buffer))
			{
				Debug.Log("Here");

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
			List<Vector3> currentLayer = new();

			// 1. Строим нижний слой
			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < columns; col++)
				{
					float offsetX = (columns - 1) * sizeX / 2f;
					float offsetZ = (rows - 1) * sizeZ / 2f;

					Vector3 pos = new Vector3(
						col * sizeX - offsetX,
						0f,
						row * sizeZ - offsetZ
					);

					currentLayer.Add(pos);
					result.Add(pos);
				}
			}

			for (int layer = 1; layer < layers; layer++)
			{
				List<Vector3> nextLayer = new();
				int cols = columns - layer;
				int rws = rows - layer;

				for (int row = 0; row < rws; row++)
				{
					for (int col = 0; col < cols; col++)
					{
						int baseCols = columns - (layer - 1);

						int i00 = row * baseCols + col;
						int i01 = row * baseCols + (col + 1);
						int i10 = (row + 1) * baseCols + col;
						int i11 = (row + 1) * baseCols + (col + 1);

						if (i00 >= currentLayer.Count || i01 >= currentLayer.Count ||
						    i10 >= currentLayer.Count || i11 >= currentLayer.Count)
							continue;

						Vector3 center = (
							currentLayer[i00] +
							currentLayer[i01] +
							currentLayer[i10] +
							currentLayer[i11]
						) / 4f;

						center.y = layer * sizeY;

						nextLayer.Add(center);
						result.Add(center);
					}
				}

				currentLayer = nextLayer;
			}

			return result;
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