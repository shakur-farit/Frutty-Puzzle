using System.Collections.Generic;
using Code.Gameplay.Common.Collisions;
using UnityEngine;

namespace Code.Gameplay.Common.Time
{
	public class PhysicsService : IPhysicsService
	{
		private static readonly RaycastHit[] Hits = new RaycastHit[128];
		private static readonly Collider[] OverlapHits = new Collider[128];

		private readonly ICollisionRegistry _collisionRegistry;

		public PhysicsService(ICollisionRegistry collisionRegistry)
		{
			_collisionRegistry = collisionRegistry;
		}

		public IEnumerable<GameEntity> RaycastAll(Vector3 worldPosition, Vector3 direction, int layerMask)
		{
			int hitCount = Physics.RaycastNonAlloc(worldPosition, direction, Hits, layerMask);

			for (int i = 0; i < hitCount; i++)
			{
				RaycastHit hit = Hits[i];
				if (hit.collider == null)
					continue;

				GameEntity entity = _collisionRegistry.Get<GameEntity>(hit.collider.GetInstanceID());
				if (entity == null)
					continue;

				yield return entity;
			}
		}

		public GameEntity Raycast(Vector3 worldPosition, Vector3 direction, int layerMask)
		{
			int hitCount = Physics.RaycastNonAlloc(worldPosition, direction, Hits, Mathf.Infinity, layerMask);

			GameEntity closestEntity = null;
			float closestDistance = Mathf.Infinity;

			for (int i = 0; i < hitCount; i++)
			{
				RaycastHit hit = Hits[i];

				if (hit.collider == null)
					continue;

				GameEntity entity = _collisionRegistry.Get<GameEntity>(hit.collider.GetInstanceID());
				if (entity == null)
					continue;

				if (hit.distance < closestDistance)
				{
					closestDistance = hit.distance;
					closestEntity = entity;
				}
			}

			return closestEntity;
		}

		public GameEntity LineCast(Vector3 start, Vector3 end, int layerMask)
		{
			int hitCount = Physics.RaycastNonAlloc(start, end, Hits, layerMask);

			for (int i = 0; i < hitCount; i++)
			{
				RaycastHit hit = Hits[i];
				if (hit.collider == null)
					continue;

				GameEntity entity = _collisionRegistry.Get<GameEntity>(hit.collider.GetInstanceID());
				if (entity == null)
					continue;

				return entity;
			}

			return null;
		}

		public IEnumerable<GameEntity> SphereCast(Vector3 position, float radius, int layerMask)
		{
			int hitCount = OverlapSphere(position, radius, OverlapHits, layerMask);

			DrawDebug(position, radius, 1f, Color.red);

			for (int i = 0; i < hitCount; i++)
			{
				GameEntity entity = _collisionRegistry.Get<GameEntity>(OverlapHits[i].GetInstanceID());
				if (entity == null)
					continue;

				yield return entity;
			}
		}

		public int SphereCastNonAlloc(Vector3 position, float radius, int layerMask, GameEntity[] hitBuffer)
		{
			int hitCount = OverlapSphere(position, radius, OverlapHits, layerMask);

			DrawDebug(position, radius, 1f, Color.green);

			for (int i = 0; i < hitCount; i++)
			{
				GameEntity entity = _collisionRegistry.Get<GameEntity>(OverlapHits[i].GetInstanceID());
				if (entity == null)
					continue;

				if (i < hitBuffer.Length)
					hitBuffer[i] = entity;
			}

			return hitCount;
		}

		public TEntity OverlapPoint<TEntity>(Vector3 worldPosition, int layerMask) where TEntity : class
		{
			float radius = 0.01f;
			int hitCount = Physics.OverlapSphereNonAlloc(worldPosition, radius, OverlapHits, layerMask);

			for (int i = 0; i < hitCount; i++)
			{
				Collider hit = OverlapHits[i];
				if (hit == null)
					continue;

				TEntity entity = _collisionRegistry.Get<TEntity>(hit.GetInstanceID());
				if (entity == null)
					continue;

				return entity;
			}

			return null;
		}

		public int OverlapSphere(Vector3 worldPos, float radius, Collider[] hits, int layerMask) =>
			Physics.OverlapSphereNonAlloc(worldPos, radius, hits, layerMask);

		private static void DrawDebug(Vector3 worldPos, float radius, float seconds, Color color)
		{
			Debug.DrawRay(worldPos, radius * Vector3.up, color, seconds);
			Debug.DrawRay(worldPos, radius * Vector3.down, color, seconds);
			Debug.DrawRay(worldPos, radius * Vector3.left, color, seconds);
			Debug.DrawRay(worldPos, radius * Vector3.right, color, seconds);
		}
	}
}