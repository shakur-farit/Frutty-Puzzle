using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.States.StateInfrastructure
{
	public class EndOfFrameExitState : IState, IUpdateable
	{
		private UniTaskCompletionSource _exitCompletionSource;

		private bool ExitWasRequested => _exitCompletionSource != null;

		public virtual void Enter()
		{
		}

		protected virtual void ExitOnEndOfFrame()
		{
		}

		protected virtual void OnUpdate()
		{
		}

		UniTask IExitableState.BeginExit()
		{
			_exitCompletionSource = new UniTaskCompletionSource();
			return _exitCompletionSource.Task;
		}

		async UniTask IExitableState.EndExit()
		{
			await WaitForEndOfFrame();
			ClearExitTask();
		}

		protected virtual async UniTask WaitForEndOfFrame()
		{
			await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);

			ExitOnEndOfFrame();
		}

		void IUpdateable.Update()
		{
			if (ExitWasRequested == false)
				OnUpdate();

			if (ExitWasRequested)
				ResolveExitPromise();
		}

		private void ResolveExitPromise() =>
			_exitCompletionSource?.TrySetResult();

		private void ClearExitTask() =>
			_exitCompletionSource = null;
	}
}