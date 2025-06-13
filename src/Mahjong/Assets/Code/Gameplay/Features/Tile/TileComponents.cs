using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Tile
{
	[Game] public class TileTypeIdComponent : IComponent { public TileTypeId Value; }

	[Game] public class TileSpriteRenderer : IComponent { public SpriteRenderer Value; }

	[Game] public class Acorn : IComponent { }
	[Game] public class Amanita : IComponent { }
}