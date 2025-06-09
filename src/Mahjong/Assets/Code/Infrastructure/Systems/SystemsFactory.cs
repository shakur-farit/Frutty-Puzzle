using Entitas;
using Zenject;

namespace Code.Infrastructure.Systems
{
	public class SystemsFactory : ISystemsFactory
	{
		private readonly DiContainer _container;

		public SystemsFactory(DiContainer container) =>
			_container = container;

		public T Create<T>() where T : ISystem =>
			_container.Instantiate<T>();

		public T Create<T>(params object[] args) where T : ISystem =>
			_container.Instantiate<T>(args);
	}
}