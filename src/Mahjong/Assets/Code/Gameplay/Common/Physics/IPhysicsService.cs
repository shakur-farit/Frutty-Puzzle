using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Common.Time
{
	public interface IPhysicsService
	{
		GameEntity Raycast(Vector3 worldPosition, Vector3 direction, int layerMask);
		GameEntity LineCast(Vector3 start, Vector3 end, int layerMask);
		TEntity OverlapPoint<TEntity>(Vector3 worldPosition, int layerMask) where TEntity : class;
		IEnumerable<GameEntity> RaycastAll(Vector3 worldPosition, Vector3 direction, int layerMask);
		IEnumerable<GameEntity> SphereCast(Vector3 position, float radius, int layerMask);
		int OverlapSphere(Vector3 worldPos, float radius, Collider[] hits, int layerMask);
		int SphereCastNonAlloc(Vector3 position, float radius, int layerMask, GameEntity[] hitBuffer);
	}
}