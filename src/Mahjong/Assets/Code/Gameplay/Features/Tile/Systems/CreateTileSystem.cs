using System.Collections.Generic;
using Code.Gameplay.Features.Tile.Factory;
using Entitas;

namespace Code.Gameplay.Features.Tile
{
	public class CreateTileSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(1);

		private readonly IGroup<GameEntity> _levels;
		private readonly IGroup<GameEntity> _grids;

		private readonly ITileFactory _tileFactory;

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
		}

		public void Execute()
		{
			foreach (GameEntity level in _levels.GetEntities(_buffer))
			foreach (GameEntity grid in _grids)
			{
				for (int i = 0; i < level.TilePairsOnLevel; i++)
				{
					int index = i * 2;

					_tileFactory.CreateTile(TileTypeId.Acorn, grid.CellPositions[index]);
					_tileFactory.CreateTile(TileTypeId.Acorn, grid.CellPositions[index + 1]);
				}

				level.RemoveTilePairsOnLevel();
			}
		}
	}
}