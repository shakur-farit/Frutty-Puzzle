using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Grid
{
	public class SquareGridReactiveSystem : ReactiveSystem<GameEntity>
	{
		public SquareGridReactiveSystem(IContext<GameEntity> context) : base(context)
		{
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.AllOf(
					GameMatcher.Square,
					GameMatcher.GridColumns,
					GameMatcher.GridRows,
					GameMatcher.GridLayers,
					GameMatcher.CellSizeX,
					GameMatcher.CellSizeY,
					GameMatcher.CellSizeZ)
				.Added());
		}

		protected override bool Filter(GameEntity entity) => 
			entity.isSquare 
			&& entity.hasGridColumns && entity.hasGridRows && entity.hasGridLayers
			&& entity.hasCellSizeX && entity.hasCellSizeY && entity.hasCellSizeZ;

		protected override void Execute(List<GameEntity> entities)
		{
		}


		public List<Vector3> GeneratePositions()
		{
			
			return new();
		}
	}
}