namespace Code.Meta.UI.Windows.Service
{
	public interface IWindowService
	{
		void Open(WindowId windowId);
		void Close(WindowId windowId);
	}
}