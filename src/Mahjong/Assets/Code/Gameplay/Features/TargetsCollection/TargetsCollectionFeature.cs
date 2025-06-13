using Code.Gameplay.Features.TargetsCollection.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.TargetsCollection
{
	public sealed class TargetsCollectionFeature : Feature
	{
		public TargetsCollectionFeature(ISystemsFactory systems)
		{
			Add(systems.Create<CollectTargetOnButtonMouseClickSystem>());
		}
	}
}