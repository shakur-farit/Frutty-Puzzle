using System.Collections.Generic;
using Code.Gameplay.Features.Tile.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Tile.Systems
{
	public class CreateTileSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(1);

		private readonly IGroup<GameEntity> _levels;
		private readonly IGroup<GameEntity> _grids;

		private readonly ITileFactory _tileFactory;
		private readonly IGroup<GameEntity> _lockControllers;

		public CreateTileSystem(GameContext game, ITileFactory tileFactory)
		{
			_tileFactory = tileFactory;
			_levels = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TilePairsOnLevel));

			_grids = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Grid,
					GameMatcher.Available,
					GameMatcher.CellPositions));

			_lockControllers = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.PositionByTile));
		}

		public void Execute()
		{
			foreach (GameEntity level in _levels.GetEntities(_buffer))
			foreach (GameEntity grid in _grids)
			foreach (GameEntity controller in _lockControllers)
			{
				for (int i = 0; i < level.TilePairsOnLevel; i++)
				{
					int index = i * 2;

					GameEntity firstTile = CreateTile(grid.CellPositions[index]);
					GameEntity secondTile = CreateTile(grid.CellPositions[index + 1]);
					SaveTilePosition(controller, firstTile.Id, grid.CellPositions[index]);
					SaveTilePosition(controller, secondTile.Id, grid.CellPositions[index + 1]);
				}


				level.RemoveTilePairsOnLevel();
			}
		}

		private GameEntity CreateTile(Vector3 position) =>
			_tileFactory.CreateTile(TileTypeId.Acorn, position);

		private void SaveTilePosition(GameEntity controller, int tileId, Vector3 position) =>
			controller.PositionByTile.Add(tileId, position);
	}
}