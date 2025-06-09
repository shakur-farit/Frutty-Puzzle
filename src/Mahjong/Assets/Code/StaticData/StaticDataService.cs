using Code.Infrastructure.AsstesManagement;
using Code.Infrastructure.States.GameStates;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Code.Meta.UI.Windows;
using Code.Meta.UI.Windows.Config;

namespace Code.StaticData
{
	public class StaticDataService : IStaticDataService
	{
		private const string WindowConfigLabel = "WindowConfig";

		private Dictionary<WindowId, WindowConfig> _windowById;

		private readonly IAssetProvider _assetProvider;

		public StaticDataService(IAssetProvider assetProvider) => 
			_assetProvider = assetProvider;

		public async UniTask Load()
		{ 
			await LoadWindows();
		}

		public WindowConfig GetWindowConfig(WindowId id)
		{
			if (_windowById.TryGetValue(id, out WindowConfig config))
				return config;

			throw new Exception($"Window config for {id} was not found");
		}

		private async UniTask LoadWindows() =>
			_windowById = (await _assetProvider.LoadAll<WindowConfig>(WindowConfigLabel))
				.ToDictionary(x => x.TypeId, x => x);
	}
}