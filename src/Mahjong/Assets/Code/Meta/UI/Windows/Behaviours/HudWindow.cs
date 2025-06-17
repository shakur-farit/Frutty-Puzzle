using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.UI.Windows.Service;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.Windows.Behaviours
{
	public class HudWindow : BaseWindow
	{
		[SerializeField] private Button _nextLevelButton;
		[SerializeField] private Button _restartLevelButton;

		private IGameStateMachine _stateMachine;
		private IWindowService _windowService;

		[Inject]
		public void Constructor(IGameStateMachine stateMachine, IWindowService windowService)
		{
			Id = WindowId.Hud;

			_stateMachine = stateMachine;
			_windowService = windowService;
		}

		protected override void Initialize()
		{
			_nextLevelButton.onClick.AddListener(StartNextLevel);
			_nextLevelButton.onClick.AddListener(CloseWindow);
			_restartLevelButton.onClick.AddListener(RequestRestart);
		}

		private void StartNextLevel() =>
			_stateMachine.Enter<GameplayEnterState>();

		private void CloseWindow() =>
			_windowService.Close(WindowId.Hud);

		private void RequestRestart()
		{
			CreateEntity.Empty()
				.With(x => x.isRestartRequested = true);
		}
	}
}