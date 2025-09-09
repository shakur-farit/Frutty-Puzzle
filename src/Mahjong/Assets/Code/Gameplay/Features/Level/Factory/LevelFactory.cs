using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Level.Config;
using Code.Infrastructure.Identifiers;
using Code.StaticData;

namespace Code.Gameplay.Features.Level.Factory
{
	public class LevelFactory : ILevelFactory
	{
		private readonly IIdentifierService _identifier;
		private readonly IStaticDataService _staticDataService;

		public LevelFactory(IIdentifierService identifier, IStaticDataService staticDataService)
		{
			_identifier = identifier;
			_staticDataService = staticDataService;
		}

		public GameEntity CreateLevel(LevelId id)
		{
			LevelConfig config = _staticDataService.GetLevelConfig(id);

			return CreateEntity.Empty()
				.AddId(_identifier.Next())
				.AddLevelId(id)
				.AddGridTypeOnLevel(config.GridType)
				.AddTilePairsOnLevel(config.TilePairs)
				;
		}
	}
}