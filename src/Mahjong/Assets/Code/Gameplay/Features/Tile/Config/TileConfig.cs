using Code.Infrastructure.View.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Features.Tile.Config
{
	[CreateAssetMenu(menuName = "Mohjong/Tile Config", fileName = "TileConfig")]
	public class TileConfig : ScriptableObject
	{
		public TileTypeId Id;
		public Sprite Sprite;
		public EntityBehaviour PrefabView;
	}
}