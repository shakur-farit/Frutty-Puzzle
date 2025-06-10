using Code.Common.Entity;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Features.Level.Factory
{
	public class LevelFactory : ILevelFactory
	{
		private readonly IIdentifierService _identifier;

		public LevelFactory(IIdentifierService identifier) => 
			_identifier = identifier;

		public void CreateLevel() =>
			CreateEntity.Empty()
				.AddId(_identifier.Next())
				.AddTilesInLevel(6)
			;
	}
}