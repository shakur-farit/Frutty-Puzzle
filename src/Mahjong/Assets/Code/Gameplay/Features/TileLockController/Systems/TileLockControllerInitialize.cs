using Code.Common.Entity;
using Entitas;

namespace Code.Gameplay.Features.TileLockController.Systems
{
	public class TileLockControllerInitialize : IInitializeSystem
	{
		public void Initialize()
		{
			CreateEntity.Empty()
				.AddPositionByTile(new())
				;
		}
	}
}