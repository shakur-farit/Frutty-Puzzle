using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TileLockController.Systems
{
	public class MarkTileUnlockedOrLockedSystem : IExecuteSystem
	{
		private readonly GameContext _game;
		private readonly IGroup<GameEntity> _lockControllers;
		private readonly IGroup<GameEntity> _grid;

		public MarkTileUnlockedOrLockedSystem(GameContext game)
		{
			_game = game;
			_lockControllers = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.PositionByTile));

			_grid = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Grid,
					GameMatcher.CellSizeX,
					GameMatcher.CellSizeZ,
					GameMatcher.CellSizeY));
		}

		public void Execute()
		{
			foreach(GameEntity controller in _lockControllers)
			foreach(GameEntity grid in _grid)
			foreach(int tileId in controller.PositionByTile.Keys)
			{
				Vector3 position = controller.PositionByTile[tileId];
				GameEntity tile = _game.GetEntityWithId(tileId);

				bool isLocked = IsLocked(position, controller.PositionByTile, grid.CellSizeX, grid.CellSizeY, grid.CellSizeZ);

				tile.isLocked = isLocked;
				tile.isUnlocked = !isLocked;
			}
		}

		public bool IsLocked(Vector3 tilePosition, Dictionary<int, Vector3> allTiles, float sizeX,
			float sizeY, float sizeZ, float tolerance = 0.01f, float offsetTolerance = 0.71f)
		{
			bool hasLeft = false;
			bool hasRight = false;
			bool hasTop = false;

			foreach (Vector3 otherTilesPosition in allTiles.Values)
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