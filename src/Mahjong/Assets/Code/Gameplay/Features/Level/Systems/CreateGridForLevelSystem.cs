using System.Collections.Generic;
using Code.Gameplay.Features.Grid.Factory;
using Entitas;

namespace Code.Gameplay.Features.Level
{
	public class CreateGridForLevelSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _levels;

		private readonly IGridFactory _gridFactory;
		private readonly List<GameEntity> _buffer = new(1);

		public CreateGridForLevelSystem(GameContext game, IGridFactory gridFactory)
		{
			_gridFactory = gridFactory;
			_levels = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.GridTypeOnLevel)
				.NoneOf(GameMatcher.LevelAvailable));
		}

		public void Execute()
		{
			foreach (GameEntity level in _levels.GetEntities(_buffer))
			{
				_gridFactory.CreateGrid(level.GridTypeOnLevel);

				level.isLevelAvailable = true;
			}
		}
	}
}