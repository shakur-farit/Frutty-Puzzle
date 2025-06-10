using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Gameplay.Features.Tile;
using Code.Gameplay.Features.Tile.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Level.Systems
{
	public class TilesCreateSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(1);

		private readonly ITileFactory _tileFactory;
		private readonly IGroup<GameEntity> _levels;

		public TilesCreateSystem(GameContext game, ITileFactory tileFactory)
		{
			_tileFactory = tileFactory;
			_levels = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TilesInLevel)
				.NoneOf(GameMatcher.Created));
		}

		public void Execute()
		{
			foreach (GameEntity level in _levels.GetEntities(_buffer))
			{
				Vector3 position = Vector3.one;
				for (int i = 0; i < level.TilesInLevel; i++)
				{
					_tileFactory.CreateTile(TileTypeId.Acorn, position);
					position = position.AddX(1f);
				}

				level.isCreated = true;
			}
		}
	}
}