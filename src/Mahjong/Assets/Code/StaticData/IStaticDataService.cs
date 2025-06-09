using Code.Infrastructure.States.GameStates;
using Code.Meta.UI.Windows;
using Code.Meta.UI.Windows.Config;
using Cysharp.Threading.Tasks;

namespace Code.StaticData
{
	public interface IStaticDataService
	{
		UniTask Load();

		WindowConfig GetWindowConfig(WindowId id);
	}
}