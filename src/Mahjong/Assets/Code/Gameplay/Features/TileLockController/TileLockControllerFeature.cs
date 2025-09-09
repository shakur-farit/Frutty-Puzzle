using Code.Gameplay.Features.TileComparer.Systems;
using Code.Gameplay.Features.TileLockController.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.TileLockController
{
	public sealed class TileLockControllerFeature : Feature
	{
		public TileLockControllerFeature(ISystemsFactory systems)
		{
			Add(systems.Create<TileLockControllerInitialize>());

			Add(systems.Create<MarkTileUnlockedOrLockedSystem>());
			Add(systems.Create<TileUnlockingVisualSystem>());

			Add(systems.Create<CleanupTileLockControllerSystem>());
		}
	}
}