using Code.Infrastructure.Systems;
using Code.Infrastructure.View.Systems;

namespace Code.Infrastructure.View
{
	public sealed class BindViewFeature : Feature
	{
		public BindViewFeature(ISystemsFactory systems)
		{
			Add(systems.Create<BindEntityViewFromPathSystem>());
			Add(systems.Create<BindEntityViewFromPrefabSystem>());
		}
	}
}