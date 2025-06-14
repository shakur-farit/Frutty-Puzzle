using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Tile.Systems
{
	public class SelectUnlockedTilesOnCLickSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(32);

		private readonly IGroup<GameEntity> _tiles;

		public SelectUnlockedTilesOnCLickSystem(GameContext game)
		{
			_tiles = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Tile,
					GameMatcher.TileSelectIcon,
					GameMatcher.CollectedTarget,
					GameMatcher.Unlocked)
				.NoneOf(GameMatcher.Selected));
		}

		public void Execute()
		{
			foreach (GameEntity tile in _tiles.GetEntities(_buffer))
			{
				tile.TileSelectIcon.SetActive(true);
				tile.isSelected = true;
			}
		}
	}
}