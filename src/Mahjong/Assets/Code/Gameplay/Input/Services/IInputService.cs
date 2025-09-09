using UnityEngine;

namespace Code.Gameplay.Input.Services
{
	public interface IInputService
	{
		Camera CameraMain { get; }
		Vector3 GetScreenMousePosition();
		Vector3 GetWorldMousePosition();
		bool HasAxisInput();
		float GetVerticalAxis();
		float GetHorizontalAxis();
		bool GetLeftMouseButton();
		bool GetLeftMouseButtonDown();
		bool GetLeftMouseButtonUp();
	}
}