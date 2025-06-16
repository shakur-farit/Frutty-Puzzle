using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TileComparer.Systems
{
	public class ProcessLockedTilesSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _tiles;
		private readonly IGroup<GameEntity> _collectors;
		private readonly List<GameEntity> _buffer = new(1);

		public ProcessLockedTilesSystem(GameContext game)
		{
			_tiles = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Locked,
					GameMatcher.TileSpriteRenderer,
					GameMatcher.CollectedTarget));

			_collectors = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TargetsBuffer));
		}

		public void Execute()
		{
			foreach (GameEntity collector in _collectors)
			foreach (GameEntity tile in _tiles.GetEntities(_buffer))
			{
				collector.TargetsBuffer.Remove(tile.Id);
				tile.isCollectedTarget = false;
				tile.TileSpriteRenderer.color = Color.gray;
			}
		}
	}
}