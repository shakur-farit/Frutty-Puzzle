using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.Grid.Services
{
	public interface IGridLayerCentroid
	{
		Vector3 GetCentroid(List<Vector3> positions);
	}
}