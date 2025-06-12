using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.Grid.Systems
{
	public class GridLayerCentroid : IGridLayerCentroid
	{
		public Vector3 GetCentroid(List<Vector3> positions)
		{
			if (positions.Count == 0)
				return Vector3.zero;

			Vector3 sum = Vector3.zero;
			foreach (var pos in positions)
				sum += pos;

			return new Vector3(sum.x / positions.Count, 0f, sum.z / positions.Count);
		}
	}
}