using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Code.Infrastructure.AsstesManagement
{
	public class AssetProvider : IAssetProvider
	{
		private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new();
		private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

		public async UniTask Initialize() => await Addressables.InitializeAsync().ToUniTask();

		public async UniTask<T> LoadComponent<T>(string addressReference) where T : Component
		{
			GameObject prefab = await Load<GameObject>(addressReference);

			if (prefab.TryGetComponent(out T component) == false)
				throw new Exception($"Prefab {addressReference} does not have a {typeof(T).Name} component!");

			return component;
		}

		public async UniTask<T> Load<T>(string addressReference) where T : class
		{
			if (_completedCache.TryGetValue(addressReference, out AsyncOperationHandle cachedHandle))
				return cachedHandle.Result as T;

			AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(addressReference);

			AddHandle(addressReference, handle);

			try
			{
				T result = await handle.ToUniTask();
				_completedCache[addressReference] = handle;
				return result;
			}
			catch (Exception e)
			{
				Debug.LogError($"Failed to load asset {addressReference}: {e.Message}");
				_handles[addressReference].Remove(handle);
				Addressables.Release(handle);
				throw;
			}
		}

		public async UniTask<List<T>> LoadAll<T>(string label) where T : class
		{
			if (_completedCache.TryGetValue(label, out AsyncOperationHandle cachedHandle))
				return new List<T>((IList<T>)cachedHandle.Result);

			AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(label, null);

			AddHandle(label, handle);

			try
			{
				IList<T> result = await handle.ToUniTask();
				_completedCache[label] = handle;
				return new List<T>(result);
			}
			catch (Exception e)
			{
				Debug.LogError($"Failed to load assets with label {label}: {e.Message}");
				_handles[label].Remove(handle);
				Addressables.Release(handle);
				throw;
			}
		}

		public void Release(string key)
		{
			if (_completedCache.TryGetValue(key, out AsyncOperationHandle handle))
			{
				Addressables.Release(handle);
				_completedCache.Remove(key);
			}

			if (_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
			{
				foreach (AsyncOperationHandle h in resourceHandles)
					Addressables.Release(h);

				_handles.Remove(key);
			}
		}

		public void CleanUp()
		{
			foreach (AsyncOperationHandle handle in _completedCache.Values)
				Addressables.Release(handle);

			foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
			foreach (AsyncOperationHandle handle in resourceHandles)
				Addressables.Release(handle);

			_completedCache.Clear();
			_handles.Clear();
		}

		private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
		{
			if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
			{
				resourceHandles = new List<AsyncOperationHandle>();
				_handles[key] = resourceHandles;
			}

			resourceHandles.Add(handle);
		}
	}
}