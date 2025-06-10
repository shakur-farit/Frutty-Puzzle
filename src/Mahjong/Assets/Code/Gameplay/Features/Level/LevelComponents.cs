using Entitas;

namespace Code.Gameplay.Features.Level
{
	[Game] public class LevelSize : IComponent { public float Value; }
	[Game] public class TilesInLevel : IComponent { public int Value; }
	[Game] public class Created : IComponent { }
}