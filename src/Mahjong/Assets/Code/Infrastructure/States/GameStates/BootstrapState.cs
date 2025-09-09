using Code.Infrastructure.AsstesManagement;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.States.GameStates
{
	public class BootstrapState : SimpleState
	{
		private readonly IGameStateMachine _stateMachine;
		private readonly IAssetProvider _assetProvider;

		public BootstrapState(IGameStateMachine stateMachine, IAssetProvider assetProvider)
		{
			_stateMachine = stateMachine;
			_assetProvider = assetProvider;
		}

		public override async void Enter()
		{
			await InitAddressables();
			EnterToInitializeProgressState();
		}

		private async UniTask InitAddressables() =>
			await _assetProvider.Initialize();

		private void EnterToInitializeProgressState() =>
			_stateMachine.Enter<InitializeProgressState>();
	}
}