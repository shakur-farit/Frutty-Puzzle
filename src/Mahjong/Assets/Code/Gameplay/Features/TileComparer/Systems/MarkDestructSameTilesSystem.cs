using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.TileComparer
{
	public class MarkDestructSameTilesSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(32);
		private readonly IGroup<GameEntity> _tiles;

		public MarkDestructSameTilesSystem(GameContext game)
		{
			_tiles = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Tile,
					GameMatcher.Same));
		}

		public void Execute()
		{
			foreach (GameEntity tile in _tiles.GetEntities(_buffer))
			{
				tile.isProcessedTarget = true;
				tile.isDestructed = true;
			}
		}
	}
}