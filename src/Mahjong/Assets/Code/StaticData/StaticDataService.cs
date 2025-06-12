using Code.Infrastructure.AsstesManagement;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Grid;
using Code.Gameplay.Features.Grid.Config;
using Code.Gameplay.Features.Level;
using Code.Gameplay.Features.Level.Config;
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
		private const string GridConfigLabel = "GridConfig";
		private const string LevelConfigLabel = "LevelConfig";

		private Dictionary<WindowId, WindowConfig> _windowById;
		private Dictionary<TileTypeId, TileConfig> _tileById;
		private Dictionary<GridTypeId, GridConfig>  _gridById;
		private Dictionary<LevelId, LevelConfig>  _levelById;

		private readonly IAssetProvider _assetProvider;

		public StaticDataService(IAssetProvider assetProvider) => 
			_assetProvider = assetProvider;

		public async UniTask Load()
		{ 
			await LoadWindows();
			await LoadTiles();
			await LoadGrids();
			await LoadLevels();
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

		public GridConfig GetGridConfig(GridTypeId id)
		{
			if (_gridById.TryGetValue(id, out GridConfig config))
				return config;

			throw new Exception($"Grid config for {id} was not found");
		}

		public LevelConfig GetLevelConfig(LevelId id)
		{
			if (_levelById.TryGetValue(id, out LevelConfig config))
				return config;

			throw new Exception($"Level config for {id} was not found");
		}

		private async UniTask LoadWindows() =>
			_windowById = (await _assetProvider.LoadAll<WindowConfig>(WindowConfigLabel))
				.ToDictionary(x => x.Id, x => x);

		private async UniTask LoadTiles() =>
			_tileById = (await _assetProvider.LoadAll<TileConfig>(TileConfigLabel))
				.ToDictionary(x => x.Id, x => x);

		private async UniTask LoadGrids() =>
			_gridById = (await _assetProvider.LoadAll<GridConfig>(GridConfigLabel))
				.ToDictionary(x => x.Id, x => x);

		private async UniTask LoadLevels() =>
			_levelById = (await _assetProvider.LoadAll<LevelConfig>(LevelConfigLabel))
				.ToDictionary(x => x.Id, x => x);
	}
}