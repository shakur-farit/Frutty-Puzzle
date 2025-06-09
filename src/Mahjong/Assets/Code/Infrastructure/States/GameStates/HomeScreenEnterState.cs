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
		private readonly IGameStateMachine _stateMachine;

		public HomeScreenEnterState(IWindowService windowService, IGameStateMachine stateMachine)
		{
			_windowService = windowService;
			_stateMachine = stateMachine;
		}


		public override void Enter()
		{
			OpenMainMenuWindow();
			EnterToHomeScreenState();
		}

		private async void EnterToHomeScreenState() =>
			await _stateMachine.Enter<HomeScreenState>();

		private void OpenMainMenuWindow() =>
			_windowService.Open(WindowId.MainMenuWindow);
	}
}