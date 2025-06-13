using Code.Gameplay.Input.Services;
using Entitas;

namespace Code.Gameplay.Input.Systems
{
	public class EmitInputSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _inputs;

		private readonly IInputService _input;

		public EmitInputSystem(GameContext game, IInputService input)
		{
			_input = input;
			_inputs = game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Input));
		}

		public void Execute()
		{
			foreach (GameEntity input in _inputs)
			{
				input.isLeftMouseClicked = _input.GetLeftMouseButtonDown();
				input.ReplaceScreenMousePosition(_input.GetScreenMousePosition());
			}
		}
	}
}