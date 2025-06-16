using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Tile
{
	[Game] public class Tile : IComponent { }
	[Game] public class TileTypeIdComponent : IComponent { public TileTypeId Value; }

	[Game] public class TileSpriteRenderer : IComponent { public SpriteRenderer Value; }
	[Game] public class TileSelectIcon : IComponent { public GameObject Value; }
	[Game] public class TileSizeX : IComponent { public float Value; }
	[Game] public class TileSizeY : IComponent { public float Value; }
	[Game] public class TileSizeZ : IComponent { public float Value; }
	[Game] public class Selected : IComponent { }
	[Game] public class TilesCreated : IComponent { }
}