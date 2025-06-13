using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.TargetsCollection
{
	public class TargetCollectionComponents
	{
		[Game] public class TargetsBuffer : IComponent { public List<int> Value; }
		[Game] public class LayerMask : IComponent { public int Value; }
	}
}