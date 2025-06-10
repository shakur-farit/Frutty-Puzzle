using Code.Gameplay.Features.Level.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Level
{
	public sealed class LevelFeature : Feature
	{
		public LevelFeature(ISystemsFactory systems)
		{
			Add(systems.Create<TilesCreateSystem>());
		}
	}
}