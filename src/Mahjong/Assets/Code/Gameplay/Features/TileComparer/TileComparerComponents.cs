using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.TileComparer
{
	[Game] public class TileCompareList : IComponent { public List<int> Value; }
	[Game] public class CompareListLimit : IComponent { public int Value; }
	[Game] public class CompareListFull : IComponent { }
	[Game] public class Same : IComponent { }
	[Game] public class NotSame : IComponent { }
}