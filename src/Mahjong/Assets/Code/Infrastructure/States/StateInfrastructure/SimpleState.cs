using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.States.StateInfrastructure
{
	public class SimpleState : IState
	{
		public virtual void Enter()
		{
		}

		protected virtual void Exit()
		{
		}

		UniTask IExitableState.BeginExit()
		{
			Exit();
			return UniTask.CompletedTask;
		}

		UniTask IExitableState.EndExit() =>
			UniTask.CompletedTask;
	}
}