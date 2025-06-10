using Code.Gameplay.Common.Collisions;
using Code.Infrastructure.View.Registrars;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.View.Behaviours
{
	public class EntityBehaviour : MonoBehaviour, IEntityView
	{
		private ICollisionRegistry _collisionRegistry;

		public GameEntity Entity { get; private set; }

		[Inject]
		public void Constructor(ICollisionRegistry collisionRegistry) =>
			_collisionRegistry = collisionRegistry;

		public void SetEntity(GameEntity entity)
		{
			Entity = entity;
			Entity.AddView(this);
			Entity.Retain(this);

			foreach (IEntityComponentRegistrar registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
				registrar.RegisterComponents();

			foreach (Collider2D collider2d in GetComponentsInChildren<Collider2D>(includeInactive: true))
				_collisionRegistry.Register(collider2d.GetInstanceID(), Entity);
		}

		public void ReleaseEntity()
		{
			foreach (IEntityComponentRegistrar registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
				registrar.UnregisterComponents();

			foreach (Collider2D collider2d in GetComponentsInChildren<Collider2D>(includeInactive: true))
				_collisionRegistry.Unregister(collider2d.GetInstanceID());

			Entity.Release(this);
			Entity = null;
		}
	}
}