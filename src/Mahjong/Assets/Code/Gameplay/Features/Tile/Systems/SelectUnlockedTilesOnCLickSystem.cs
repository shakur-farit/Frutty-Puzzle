using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Tile.Systems
{
	public class SelectUnlockedTilesOnCLickSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(32);

		private readonly IGroup<GameEntity> _tiles;
		private readonly IGroup<GameEntity> _comparers;

		public SelectUnlockedTilesOnCLickSystem(GameContext game)
		{
			_tiles = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Tile,
					GameMatcher.TileSelectIcon,
					GameMatcher.CollectedTarget,
					GameMatcher.Unlocked)
				.NoneOf(GameMatcher.Selected));


			_comparers = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TileCompareList)
				.NoneOf(GameMatcher.CompareListFull));
		}

		public void Execute()
		{
			foreach (GameEntity tile in _tiles.GetEntities(_buffer))
			foreach (GameEntity comparer in _comparers)
			{
				tile.TileSelectIcon.SetActive(true);
				tile.isSelected = true;

				comparer.TileCompareList.Add(tile.Id);
			}
		}
	}
}