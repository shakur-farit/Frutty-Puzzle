using System.Collections.Generic;
using Code.Gameplay.Features.Tile.Factory;
using Code.Gameplay.Features.TileLockController.Systems;
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
		private readonly IRandomTileGenerator _generator;
		private readonly IGroup<GameEntity> _lockControllers;

		public CreateTileSystem(GameContext game, ITileFactory tileFactory, IRandomTileGenerator generator)
		{
			_tileFactory = tileFactory;
			_generator = generator;
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
				var list = _generator.GenerateStructuredTiles(level.TilePairsOnLevel, grid.CellPositions, grid.CellSizeY);

				foreach (TilePositions tilePositions in list)
				{
					Debug.Log($"{tilePositions.Id} on {tilePositions.Position}");

					var tile = CreateTile(tilePositions.Id, tilePositions.Position);
					SaveTilePosition(controller, tile.Id, tilePositions.Position);
				}

				//for (int i = 0; i < level.TilePairsOnLevel; i++)
				//{
				//	int index = i * 2;

				//	GameEntity firstTile = CreateTile(grid.CellPositions[index]);
				//	GameEntity secondTile = CreateTile(grid.CellPositions[index + 1]);
				//	SaveTilePosition(controller, firstTile.Id, grid.CellPositions[index]);
				//	SaveTilePosition(controller, secondTile.Id, grid.CellPositions[index + 1]);
				//}

				level.RemoveTilePairsOnLevel();
			}
		}

		private GameEntity CreateTile(TileTypeId id, Vector3 position) =>
			_tileFactory.CreateTile(id, position);

		private void SaveTilePosition(GameEntity controller, int tileId, Vector3 position) =>
			controller.PositionByTile.Add(tileId, position);
	}
}