using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TileComparer.Systems
{
	public class UnselectNotSameTiles : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(32);

		private readonly IGroup<GameEntity> _tiles;

		public UnselectNotSameTiles(GameContext game)
		{
			_tiles = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Tile,
					GameMatcher.TileSelectIcon,
					GameMatcher.CollectedTarget,
					GameMatcher.Selected,
					GameMatcher.NotSame));
		}

		public void Execute()
		{
			foreach (GameEntity tile in _tiles.GetEntities(_buffer))
			{
				tile.TileSelectIcon.SetActive(false);
				tile.isSelected = false;
				tile.isCollectedTarget = false;
				tile.isNotSame = false;
			}
		}
	}
}