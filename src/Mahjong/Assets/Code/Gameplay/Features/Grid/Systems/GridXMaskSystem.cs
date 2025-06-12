using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Gameplay.Features.Grid.Systems
{
	public class GridXMaskSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _grids;

		public GridXMaskSystem(GameContext game)
		{
			_grids = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Available,
					GameMatcher.CellPositions,
					GameMatcher.GridColumns,
					GameMatcher.GridRows,
					GameMatcher.GridLayers,
					GameMatcher.CellSizeX,
					GameMatcher.CellSizeY,
					GameMatcher.CellSizeZ));
		}

		public void Execute()
		{
			foreach (GameEntity grid in _grids)
			{
				List<Vector3> positions = 
					GetPositions(grid.CellPositions, 
						grid.GridColumns, grid.GridRows, grid.GridLayers,
						grid.CellSizeX, grid.CellSizeY, grid.CellSizeZ);

				grid.ReplaceCellPositions(positions);
				TryVisualizeGrid(positions);

				Debug.Log(positions.Count);

			}
		}

		private List<Vector3> GetPositions(List<Vector3> positions, int columns, int rows, int layers,
			float sizeX, float sizeY, float sizeZ)
		{
			var validIndices = new HashSet<Vector2Int>();

			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < columns; col++)
				{
					if (Mathf.Abs(row - col) <= 1 || Mathf.Abs(row - (columns - col - 1)) <= 1)
					{
						validIndices.Add(new Vector2Int(col, row));
					}
				}
			}

			var result = new List<Vector3>();

			foreach (Vector3 pos in positions)
			{
				int col = Mathf.RoundToInt((pos.x + (columns - 1) * 0.5f * sizeX) / sizeX);
				int row = Mathf.RoundToInt((pos.z + (rows - 1) * 0.5f * sizeZ) / sizeZ);

				if (validIndices.Contains(new Vector2Int(col, row)))
				{
					result.Add(pos);
				}
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