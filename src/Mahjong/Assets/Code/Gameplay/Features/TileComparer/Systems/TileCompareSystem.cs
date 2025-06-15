using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Tile;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TileComparer
{
	public class TileCompareSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(1);

		private readonly GameContext _game;
		private readonly IGroup<GameEntity> _comparers;

		public TileCompareSystem(GameContext game)
		{
			_game = game;
			_comparers = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TileCompareList,
					GameMatcher.CompareListFull));
		}

		public void Execute()
		{
			foreach (GameEntity comparer in _comparers.GetEntities(_buffer))
			{
				List<TileTypeId> compareTypeList = new List<TileTypeId>();

				foreach (int id in comparer.TileCompareList)
				{
					GameEntity tile = _game.GetEntityWithId(id);

					compareTypeList.Add(tile.TileTypeId);
				}

				if (IsSame(compareTypeList))
				{
					foreach (int id in comparer.TileCompareList)
					{
						GameEntity tile = _game.GetEntityWithId(id);

						tile.isSame = true;
					}
				}

				comparer.TileCompareList.Clear();
				comparer.isCompareListFull = false;
			}
		}

		private bool IsSame(List<TileTypeId> list) =>
			list.All(id => id == list[0]);
	}
}