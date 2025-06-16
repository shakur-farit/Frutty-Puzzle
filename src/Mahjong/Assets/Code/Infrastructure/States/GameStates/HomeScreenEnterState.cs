using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.UI.Windows;
using Code.Meta.UI.Windows.Service;
using UnityEditor.PackageManager.UI;

namespace Code.Infrastructure.States.GameStates
{
	public class HomeScreenEnterState : SimpleState
	{
		private readonly IWindowService _windowService;

		public HomeScreenEnterState(IWindowService windowService) => 
			_windowService = windowService;


		public override void Enter() => 
			OpenMainMenuWindow();

		private void OpenMainMenuWindow() =>
			_windowService.Open(WindowId.MainMenuWindow);
	}
}