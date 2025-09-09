using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Common.Destruct.Systems
{
	public class SelfDestructTimerSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(64);

		private readonly ITimeService _time;
		private readonly IGroup<GameEntity> _entities;

		public SelfDestructTimerSystem(GameContext game, ITimeService time)
		{
			_time = time;
			_entities = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.SelfDestructedTimer));
		}

		public void Execute()
		{
			foreach (GameEntity entity in _entities.GetEntities(_buffer))
			{
				if (entity.SelfDestructedTimer > 0)
				{
					entity.ReplaceSelfDestructedTimer(entity.SelfDestructedTimer - _time.DeltaTime);
				}
				else
				{
					entity.RemoveSelfDestructedTimer();
					entity.isDestructed = true;
				}
			}
		}
	}
}