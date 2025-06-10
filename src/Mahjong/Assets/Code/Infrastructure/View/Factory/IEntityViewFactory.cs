using Cysharp.Threading.Tasks;
using EntityBehaviour = Code.Infrastructure.View.Behaviours.EntityBehaviour;

namespace Code.Infrastructure.View.Factory
{
	public interface IEntityViewFactory
	{
		UniTask<EntityBehaviour> CreateViewForEntity(GameEntity entity);
		EntityBehaviour CreateViewForEntityFromPrefab(GameEntity entity);
	}
}