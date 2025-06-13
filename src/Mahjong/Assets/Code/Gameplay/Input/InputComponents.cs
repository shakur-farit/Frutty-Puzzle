using Entitas;
using UnityEngine;

namespace Code.Gameplay.Input
{
	[Game] public class Input : IComponent { }
	[Game] public class LeftMouseClicked : IComponent { }
	[Game] public class ScreenMousePosition : IComponent { public Vector3 Value; }
	[Game] public class CameraComponent : IComponent { public Camera Value; }
}