using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Common.Registrars
{
	public class MeshRendererRegistrar : EntityComponentRegistrar
	{
		[SerializeField] private MeshRenderer _meshRenderer;

		public override void RegisterComponents() =>
			Entity.AddMeshRenderer(_meshRenderer);

		public override void UnregisterComponents()
		{
			if (Entity.hasMeshRenderer)
				Entity.RemoveMeshRenderer();
		}
	}
}