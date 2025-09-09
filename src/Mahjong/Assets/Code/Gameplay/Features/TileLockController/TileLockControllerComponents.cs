using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.TileLockController
{
	public class PositionByTile : IComponent { public Dictionary<int, Vector3> Value; }
	public class Locked : IComponent { }
	public class Unlocked : IComponent { }
}