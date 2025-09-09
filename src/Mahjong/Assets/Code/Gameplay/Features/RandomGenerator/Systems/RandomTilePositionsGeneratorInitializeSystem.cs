using Code.Common.Entity;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.RandomGenerator.Systems
{
	public class RandomTilePositionsGeneratorInitializeSystem : IInitializeSystem
	{
		public void Initialize()
		{
			CreateEntity.Empty()
				.AddTilePositions(new())
				.With(x => x.isGenerationRequired = true)
				;
		}
	}
}