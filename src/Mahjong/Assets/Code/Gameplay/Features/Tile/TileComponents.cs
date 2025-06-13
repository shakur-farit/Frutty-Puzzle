using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Tile
{
	[Game] public class Tile : IComponent { }
	[Game] public class TileTypeIdComponent : IComponent { public TileTypeId Value; }

	[Game] public class TileSpriteRenderer : IComponent { public SpriteRenderer Value; }
	[Game] public class TileSelectIcon : IComponent { public GameObject Value; }
	[Game] public class Selected : IComponent { }

	[Game] public class Acorn : IComponent { }
	[Game] public class Amanita : IComponent { }
}