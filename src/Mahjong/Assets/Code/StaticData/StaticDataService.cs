using Code.Infrastructure.AsstesManagement;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Tile;
using Code.Gameplay.Features.Tile.Config;
using Code.Meta.UI.Windows;
using Code.Meta.UI.Windows.Config;

namespace Code.StaticData
{
	public class StaticDataService : IStaticDataService
	{
		private const string WindowConfigLabel = "WindowConfig";
		private const string TileConfigLabel = "TileConfig";

		private Dictionary<WindowId, WindowConfig> _windowById;
		private Dictionary<TileTypeId, TileConfig> _tileById;

		private readonly IAssetProvider _assetProvider;

		public StaticDataService(IAssetProvider assetProvider) => 
			_assetProvider = assetProvider;

		public async UniTask Load()
		{ 
			await LoadWindows();
			await LoadTiles();
		}

		public WindowConfig GetWindowConfig(WindowId id)
		{
			if (_windowById.TryGetValue(id, out WindowConfig config))
				return config;

			throw new Exception($"Window config for {id} was not found");
		}

		public TileConfig GetTileConfig(TileTypeId id)
		{
			if (_tileById.TryGetValue(id, out TileConfig config))
				return config;

			throw new Exception($"Tile config for {id} was not found");
		}

		private async UniTask LoadWindows() =>
			_windowById = (await _assetProvider.LoadAll<WindowConfig>(WindowConfigLabel))
				.ToDictionary(x => x.Id, x => x);

		private async UniTask LoadTiles() =>
			_tileById = (await _assetProvider.LoadAll<TileConfig>(TileConfigLabel))
				.ToDictionary(x => x.Id, x => x);
	}
}