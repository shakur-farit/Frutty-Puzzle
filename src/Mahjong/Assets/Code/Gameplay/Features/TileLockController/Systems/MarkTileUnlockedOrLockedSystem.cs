using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TileLockController.Systems
{
	public class MarkTileUnlockedOrLockedSystem : IExecuteSystem
	{
		private readonly GameContext _game;
		private readonly IGroup<GameEntity> _lockControllers;
		private readonly IGroup<GameEntity> _grids;

		public MarkTileUnlockedOrLockedSystem(GameContext game)
		{
			_game = game;
			_lockControllers = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.PositionByTile));

			_grids = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Grid,
					GameMatcher.GridColumns,
					GameMatcher.GridRows,
					GameMatcher.GridLayers,
					GameMatcher.CellSizeX,
					GameMatcher.CellSizeY,
					GameMatcher.CellSizeZ));
		}

		public void Execute()
		{
			foreach (GameEntity controller in _lockControllers)
			foreach (int tileId in controller.PositionByTile.Keys)
			{
				var position = controller.PositionByTile[tileId];
				var tile = _game.GetEntityWithId(tileId);
				if (IsLocked(position, controller.PositionByTile))
				{
					tile.isLocked = true;
					Debug.Log($"{tileId} is Locked");
				}
				else
				{
					tile.isUnlocked = true;
					Debug.Log($"{tileId} is Unlocked");
				}
				}
		}

		public  bool IsLocked(Vector3 tilePosition, Dictionary<int, Vector3> allTiles, float tolerance = 0.01f)
		{
			bool hasLeft = false;
			bool hasRight = false;

			foreach (var otherPos in allTiles.Values)
			{
				if (Mathf.Abs(otherPos.y - tilePosition.y) > tolerance || Mathf.Abs(otherPos.z - tilePosition.z) > tolerance)
					continue;

				if (Mathf.Abs(otherPos.x - (tilePosition.x - 1f)) < tolerance)
					hasLeft = true;

				if (Mathf.Abs(otherPos.x - (tilePosition.x + 1f)) < tolerance)
					hasRight = true;

				if (hasLeft && hasRight)
					return true;
			}

			return false;
		}
	}
}