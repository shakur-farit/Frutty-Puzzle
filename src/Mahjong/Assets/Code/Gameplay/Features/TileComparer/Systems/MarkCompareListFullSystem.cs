using Entitas;

namespace Code.Gameplay.Features.TileComparer.Systems
{
	public class MarkCompareListFullSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _comparers;

		public MarkCompareListFullSystem(GameContext game)
		{
			_comparers = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TileCompareList,
					GameMatcher.CompareListLimit));
		}

		public void Execute()
		{
			foreach (GameEntity comparer in _comparers)
				if (comparer.TileCompareList.Count >= comparer.CompareListLimit)
					comparer.isCompareListFull = true;
		}
	}
}