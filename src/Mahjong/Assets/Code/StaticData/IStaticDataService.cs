using Code.Gameplay.Features.Grid;
using Code.Gameplay.Features.Grid.Config;
using Code.Gameplay.Features.Tile;
using Code.Gameplay.Features.Tile.Config;
using Code.Meta.UI.Windows;
using Code.Meta.UI.Windows.Config;
using Cysharp.Threading.Tasks;

namespace Code.StaticData
{
	public interface IStaticDataService
	{
		UniTask Load();

		WindowConfig GetWindowConfig(WindowId id);
		TileConfig GetTileConfig(TileTypeId id);
		GridConfig GetGridConfig(GridTypeId id);
	}
}