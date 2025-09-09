using Code.Gameplay.Features.Movement.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Movement
{
	public sealed class MovementFeature : Feature
	{
		public MovementFeature(ISystemsFactory systems)
		{
			Add(systems.Create<UpdateTransformPositionSystem>());
		}
	}
}