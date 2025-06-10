using Code.Common.Destruct;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View;

namespace Code.Gameplay
{
	public sealed class BattleFeature : Feature
	{
		public BattleFeature(ISystemsFactory systems)
		{
			Add(systems.Create<BindViewFeature>());

			Add(systems.Create<ProcessGameDestructedFeature>());
		}
	}
}