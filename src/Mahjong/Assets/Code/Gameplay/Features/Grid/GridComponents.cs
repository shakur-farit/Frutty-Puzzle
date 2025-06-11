using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Grid
{
	[Game] public class GridTypeIdComponent : IComponent { public GridTypeId Value; }

	[Game] public class GridColumns : IComponent { public int Value; }
	[Game] public class GridRows : IComponent { public int Value; }
	[Game] public class GridLayers : IComponent { public int Value; }
	[Game] public class CellSizeX : IComponent { public float Value; }
	[Game] public class CellSizeY : IComponent { public float Value; }
	[Game] public class CellSizeZ : IComponent { public float Value; }

	[Game] public class CellPositions : IComponent { public List<Vector3> Value; }

	[Game] public class Square : IComponent { }
	[Game] public class Rhombus : IComponent { }
}