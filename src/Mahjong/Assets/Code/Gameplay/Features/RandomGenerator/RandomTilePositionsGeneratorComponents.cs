using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.RandomGenerator
{
	public class TilePositions : IComponent { public List<TilePosition> Value; }
	public class GenerationRequired : IComponent { }
	public class LevelSolvable : IComponent { }

}