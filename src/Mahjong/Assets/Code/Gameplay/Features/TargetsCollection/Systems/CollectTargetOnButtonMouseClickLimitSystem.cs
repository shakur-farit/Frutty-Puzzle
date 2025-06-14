using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TargetsCollection.Systems
{
	public class CollectTargetOnButtonMouseClickLimitSystem : IExecuteSystem
	{
		private readonly List<GameEntity> _buffer = new(1);

		private readonly IGroup<GameEntity> _inputs;
		private readonly IGroup<GameEntity> _collectors;

		private readonly IPhysicsService _physicsService;

		public CollectTargetOnButtonMouseClickLimitSystem(GameContext game, IPhysicsService physicsService)
		{
			_physicsService = physicsService;
			_inputs = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.LeftMouseClicked));

			_collectors = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TargetsBuffer,
					GameMatcher.TargetsLimit)
				.NoneOf(GameMatcher.Full));
		}

		public void Execute()
		{
			foreach (GameEntity input in _inputs)
			foreach (GameEntity collector in _collectors.GetEntities(_buffer))
			{
				Ray ray = input.Camera.ScreenPointToRay(input.ScreenMousePosition);
				GameEntity target = _physicsService.Raycast(ray.origin, ray.direction, collector.LayerMask);

				if (IsNotProcessed(collector, target))
				{
					collector.TargetsBuffer.Add(target.Id);
					target.isCollectedTarget = true;
				}

				if(collector.TargetsBuffer.Count >= collector.TargetsLimit)
					collector.isFull = true;
			}
		}

		private static bool IsNotProcessed(GameEntity collector, GameEntity target)
		{
			return collector.TargetsBuffer.Contains(target.Id) == false && target.isProcessedTarget == false;
		}
	}
}