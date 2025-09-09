using Code.Meta.UI.Windows.Behaviours;
using UnityEngine;

namespace Code.Meta.UI.Windows.Factory
{
	public interface IWindowFactory
	{
		void SetUIRoot(RectTransform uiRoot);
		BaseWindow CreateWindow(WindowId windowId);
	}
}