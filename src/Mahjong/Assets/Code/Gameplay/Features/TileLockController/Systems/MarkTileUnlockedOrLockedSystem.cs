using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TileLockController.Systems
{
	public class MarkTileUnlockedOrLockedSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _lockControllers;
		private readonly IGroup<GameEntity> _grid;

		private readonly GameContext _game;
		private readonly ITileLockOrUnlockChecker _checker;

		public MarkTileUnlockedOrLockedSystem(GameContext game , ITileLockOrUnlockChecker checker)
		{
			_game = game;
			_checker = checker;
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

				List<Vector3> positions = controller.PositionByTile.Values.ToList();

				bool isLocked = _checker.IsLocked(position, positions, grid.CellSizeX, grid.CellSizeY, grid.CellSizeZ);

				tile.isLocked = isLocked;
				tile.isUnlocked = !isLocked;
			}
		}
	}
}