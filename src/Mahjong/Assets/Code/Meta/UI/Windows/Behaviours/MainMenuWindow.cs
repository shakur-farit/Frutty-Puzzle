using Code.Infrastructure.Loading;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.UI.Windows.Service;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.Windows.Behaviours
{
	public class MainMenuWindow : BaseWindow
	{
		[SerializeField] private Button _startGameButton;

		private IGameStateMachine _stateMachine;
		private IWindowService _windowService;

		[Inject]
		public void Constructor(IGameStateMachine stateMachine, IWindowService windowService)
		{
			Id = WindowId.MainMenuWindow;

			_stateMachine = stateMachine;
			_windowService = windowService;
		}

		protected override void Initialize()
		{
			_startGameButton.onClick.RemoveAllListeners();

			_startGameButton.onClick.AddListener(EnterToBattle);
			_startGameButton.onClick.AddListener(CloseWindow);
		}
		
		private void EnterToBattle() =>
			_stateMachine.Enter<LoadingGameplayState, string>(Scenes.Gameplay);

		private void CloseWindow() =>
			_windowService.Close(WindowId.MainMenuWindow);
	}
}