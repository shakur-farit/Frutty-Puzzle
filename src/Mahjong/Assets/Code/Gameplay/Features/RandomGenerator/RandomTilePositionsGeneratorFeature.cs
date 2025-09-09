using Code.Gameplay.Features.RandomGenerator.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.RandomGenerator
{
	public sealed class RandomTilePositionsGeneratorFeature : Feature
	{
		public RandomTilePositionsGeneratorFeature(ISystemsFactory systems)
		{
			Add(systems.Create<RandomTilePositionsGeneratorInitializeSystem>());

			Add(systems.Create<CreateRandomTilePositionsSystem>());
			Add(systems.Create<GeneratedTilesToSolvabilityCheckingSystem>());
		}
	}
}