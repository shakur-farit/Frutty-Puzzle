using Code.Gameplay.Features.Grid;
using Entitas;

namespace Code.Gameplay.Features.Level
{
	[Game] public class LevelIdComponent : IComponent { public LevelId Value; }

	[Game] public class TilePairsOnLevel : IComponent { public int Value; }
	[Game] public class GridTypeOnLevel : IComponent { public GridTypeId Value; }

	[Game] public class LevelAvailable : IComponent { }
	[Game] public class RestartRequested : IComponent { }
}