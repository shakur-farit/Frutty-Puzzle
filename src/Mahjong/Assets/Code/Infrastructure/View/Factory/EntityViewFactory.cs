using System.Collections.Generic;
using Code.Infrastructure.AsstesManagement;
using Code.Infrastructure.View.Behaviours;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.View.Factory
{
	public class EntityViewFactory : IEntityViewFactory
	{
		private readonly Dictionary<GameEntity, bool> _loadingInProgress = new();
		private readonly Vector3 _farAway = new(-999, 999, 0);

		private readonly IAssetProvider _assetProvider;
		private readonly IInstantiator _instantiator;

		public EntityViewFactory(IAssetProvider assetProvider, IInstantiator instantiator)
		{
			_assetProvider = assetProvider;
			_instantiator = instantiator;
		}

		public async UniTask<EntityBehaviour> CreateViewForEntity(GameEntity entity)
		{
			if (_loadingInProgress.ContainsKey(entity) && _loadingInProgress[entity])
				return null;

			try
			{
				_loadingInProgress[entity] = true;

				EntityBehaviour viewPrefab = await _assetProvider.LoadComponent<EntityBehaviour>(entity.ViewPath);

				EntityBehaviour view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(
					viewPrefab,
					position: _farAway,
					Quaternion.identity,
					parentTransform: null);

				view.SetEntity(entity);

				return view;
			}
			finally
			{
				_loadingInProgress[entity] = false;
			}
		}

		public EntityBehaviour CreateViewForEntityFromPrefab(GameEntity entity)
		{
			EntityBehaviour view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(
				entity.ViewPrefab,
				position: _farAway,
				Quaternion.identity,
				parentTransform: null);

			view.SetEntity(entity);

			return view;
		}
	}
}