using Code.Infrastructure.Identifiers;
using Code.StaticData;
using System;
using Code.Common.Entity;
using UnityEngine;

namespace Code.Gameplay.Features.Tile
{
	public class TileFactory
	{
		private readonly IIdentifierService _identifier;
		private readonly IStaticDataService _staticDataService;

		public TileFactory(IIdentifierService identifier, IStaticDataService staticDataService)
		{
			_identifier = identifier;
			_staticDataService = staticDataService;
		}

		public GameEntity CreateTile(TileTypeId typeId)
		{
			switch (typeId)
			{
				case TileTypeId.Acorn:
					return CreateAcorn(typeId);
			}

			throw new Exception($"Tile with type id {typeId} does not exist");
		}

		private GameEntity CreateAcorn(TileTypeId typeId)
		{
			var config = _staticDataService.GetTileConfig(typeId);

			return CreateEntity.Empty()
				.AddId(_identifier.Next())
				.AddTileTypeId(typeId)
				.AddViewPrefab(config.PrefabView)
				.AddWorldPosition(Vector3.zero)
				;
		}
	}
}