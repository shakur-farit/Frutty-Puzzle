using System;
using UnityEngine;

namespace Code.Gameplay.Features.Grid.Config
{
	[CreateAssetMenu(menuName = "Mohjong/Grid Config", fileName = "GridConfig")]
	public class GridConfig : ScriptableObject
	{
		public GridTypeId Id;
		public int GridColumns;
		public int GridRows;
		public int GridLayers;
		public CellSize CellSize;
	}

	public class GridStrategy
	{
		public GridLayoutTypeId LayoutTypeId;
		public GridMaskTypeId MaskTypeId;
	}

	[Serializable]
	public class CellSize
	{
		public float CellSizeX;
		public float CellSizeY;
		public float CellSizeZ;
	}
}