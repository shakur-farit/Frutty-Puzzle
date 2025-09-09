using Code.Gameplay.Features.Grid;
using UnityEngine;

namespace Code.Gameplay.Features.Level.Config
{
	[CreateAssetMenu(menuName = "Mohjong/Level Config", fileName = "LevelConfig")]
	public class LevelConfig : ScriptableObject
	{
		public LevelId Id;
		public GridTypeId GridType;
		public int TilePairs;
	}
}