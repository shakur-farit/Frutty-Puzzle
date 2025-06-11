using Code.Common.Destruct;
using Code.Gameplay.Features.Grid;
using Code.Gameplay.Features.Movement;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View;

namespace Code.Gameplay
{
	public sealed class BattleFeature : Feature
	{
		public BattleFeature(ISystemsFactory systems)
		{
			Add(systems.Create<GridFeature>());
			Add(systems.Create<BindViewFeature>());
			Add(systems.Create<MovementFeature>());

			Add(systems.Create<ProcessGameDestructedFeature>());
		}
	}
}