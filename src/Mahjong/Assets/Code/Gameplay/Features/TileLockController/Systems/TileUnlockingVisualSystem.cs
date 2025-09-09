using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TileComparer.Systems
{
	public class TileUnlockingVisualSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _tiles;

		public TileUnlockingVisualSystem(GameContext game)
		{
			_tiles = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TileSpriteRenderer,
					GameMatcher.Unlocked));
		}

		public void Execute()
		{
			foreach (GameEntity tile in _tiles) 
				tile.TileSpriteRenderer.color = Color.white;
		}
	}
}