using System.Collections.Generic;
using Code.Gameplay.Features.RandomGenerator;
using Code.Gameplay.Features.Tile.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Tile.Systems
{
	public class CreateTileSystem : IExecuteSystem
	{
		private readonly ITileFactory _tileFactory;
		private readonly IGroup<GameEntity> _generators;
		private readonly IGroup<GameEntity> _controllers;
		private readonly List<GameEntity> _buffer = new(1);
		private readonly IGroup<GameEntity> _grids;

		public CreateTileSystem(GameContext game, ITileFactory tileFactory)
		{
			_tileFactory = tileFactory;
			_generators = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TilePositions,
					GameMatcher.LevelSolvable)
				.NoneOf(GameMatcher.TilesCreated));

			_grids = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.CellSizeX,
					GameMatcher.CellSizeY,
					GameMatcher.CellSizeZ));

			_controllers = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.PositionByTile));
		}

		public void Execute()
		{
			foreach (GameEntity generator in _generators.GetEntities(_buffer))
			foreach (GameEntity controller in _controllers)
			foreach (GameEntity grid in _grids)
			{
				List<TilePosition> list = generator.TilePositions;
				controller.PositionByTile.Clear();

				foreach (TilePosition tilePositions in list)
				{
					GameEntity tile = 
						CreateTile(tilePositions.Id, tilePositions.Position,
							grid.CellSizeX, grid.CellSizeY, grid.CellSizeZ);

					SaveTilePosition(controller.PositionByTile, tile.Id, tilePositions.Position);
				}

				generator.isTilesCreated = true;
			}
		}

		private GameEntity CreateTile(TileTypeId id, Vector3 position,
			float tileSizeX, float tileSizeY, float tileSizeZ) =>
			_tileFactory.CreateTile(id, position, tileSizeX, tileSizeY, tileSizeZ);

		private void SaveTilePosition(Dictionary<int, Vector3> positionsByTile, int tileId, Vector3 position) =>
			positionsByTile.Add(tileId, position);
	}
}