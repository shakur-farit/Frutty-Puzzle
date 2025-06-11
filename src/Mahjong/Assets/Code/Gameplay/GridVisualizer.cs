using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay
{
	[ExecuteAlways]
	public class GridVisualizer : MonoBehaviour
	{
		public List<Vector3> Positions = new();
		public float GizmoSize = 0.2f;
		public Color GizmoColor = Color.green;

		public void SetPositions(IEnumerable<Vector3> newPositions)
		{
			Positions = new List<Vector3>(newPositions);
		}

		private void OnDrawGizmos()
		{
			if (Positions == null || Positions.Count == 0)
				return;

			Gizmos.color = GizmoColor;

			foreach (var pos in Positions)
			{
				Gizmos.DrawCube(transform.position + pos, Vector3.one * GizmoSize);
			}
		}
	}
}