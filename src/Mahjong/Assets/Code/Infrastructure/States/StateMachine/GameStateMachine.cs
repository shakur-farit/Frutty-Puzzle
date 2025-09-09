using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.StateInfrastructure;
using Cysharp.Threading.Tasks;
using Zenject;
using IState = Code.Infrastructure.States.StateInfrastructure.IState;

namespace Code.Infrastructure.States.StateMachine
{
	public class GameStateMachine : ITickable, IGameStateMachine
	{
		private IExitableState _activeState;
		private readonly IStateFactory _stateFactory;

		public GameStateMachine(IStateFactory stateFactory) =>
			_stateFactory = stateFactory;

		public void Tick()
		{
			if (_activeState is IUpdateable updateableState)
				updateableState.Update();
		}

		public async UniTask Enter<TState>() where TState : class, IState =>
			await RequestEnter<TState>();

		public async UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload> =>
			await RequestEnter<TState, TPayload>(payload);

		private async UniTask<TState> RequestEnter<TState>() where TState : class, IState
		{
			TState state = await RequestChangeState<TState>();
			return EnterState(state);
		}

		private async UniTask<TState> RequestEnter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
		{
			TState state = await RequestChangeState<TState>();
			return EnterPayloadState(state, payload);
		}

		private TState EnterState<TState>(TState state) where TState : class, IState
		{
			_activeState = state;
			state.Enter();
			return state;
		}

		private TState EnterPayloadState<TState, TPayload>(TState state, TPayload payload)
			where TState : class, IPayloadState<TPayload>
		{
			_activeState = state;
			state.Enter(payload);
			return state;
		}

		private async UniTask<TState> RequestChangeState<TState>() where TState : class, IExitableState
		{
			if (_activeState != null)
			{
				await _activeState.BeginExit();
				await _activeState.EndExit();
			}

			return GetState<TState>();
		}

		private TState GetState<TState>() where TState : class, IExitableState =>
			_stateFactory.GetState<TState>();
	}
}