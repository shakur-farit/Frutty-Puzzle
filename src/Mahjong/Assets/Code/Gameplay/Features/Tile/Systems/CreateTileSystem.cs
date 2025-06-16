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

		public CreateTileSystem(GameContext game, ITileFactory tileFactory)
		{
			_tileFactory = tileFactory;
			_generators = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TilePositions,
					GameMatcher.LevelSolvable)
				.NoneOf(GameMatcher.TilesCreated));

			_controllers = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.PositionByTile));
		}

		public void Execute()
		{
			foreach (GameEntity generator in _generators.GetEntities(_buffer))
			foreach (GameEntity controller in _controllers)
			{
				List<TilePosition> list = generator.TilePositions;
					Debug.Log("CreateTiles");

				foreach (TilePosition tilePositions in list)
				{
					GameEntity tile = CreateTile(tilePositions.Id, tilePositions.Position);
					SaveTilePosition(controller.PositionByTile, tile.Id, tilePositions.Position);
				}

				generator.isTilesCreated = true;
			}
		}

		private GameEntity CreateTile(TileTypeId id, Vector3 position) =>
			_tileFactory.CreateTile(id, position);

		private void SaveTilePosition(Dictionary<int, Vector3> positionsByTile, int tileId, Vector3 position) =>
			positionsByTile.Add(tileId, position);
	}
}