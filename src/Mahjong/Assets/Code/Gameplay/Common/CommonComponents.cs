using Code.Infrastructure.View.Behaviours;
using UnityEngine;
using Entitas;

namespace Code.Gameplay.Common
{
	[Game] public class Id : IComponent { public int Value; }

	[Game] public class View : IComponent { public IEntityView Value; }
	[Game] public class ViewPath : IComponent { public string Value; }
	[Game] public class ViewPrefab : IComponent { public EntityBehaviour Value; }
	[Game] public class ViewParent : IComponent { public Transform Value; }

	[Game] public class Destructed : IComponent { }
	[Game] public class SelfDestructedTimer : IComponent { public float Value; }

	[Game] public class Parented : IComponent { }
	[Game] public class Unparented : IComponent { }
}