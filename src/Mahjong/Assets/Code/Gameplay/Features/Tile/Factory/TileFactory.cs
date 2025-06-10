using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Tile.Config;
using Code.Infrastructure.Identifiers;
using Code.StaticData;
using UnityEngine;

namespace Code.Gameplay.Features.Tile.Factory
{
	public class TileFactory : ITileFactory
	{
		private readonly IIdentifierService _identifier;
		private readonly IStaticDataService _staticDataService;

		public TileFactory(IIdentifierService identifier, IStaticDataService staticDataService)
		{
			_identifier = identifier;
			_staticDataService = staticDataService;
		}

		public GameEntity CreateTile(TileTypeId typeId, Vector3 at)
		{
			switch (typeId)
			{
				case TileTypeId.Acorn:
					return CreateAcorn(typeId, at);
				case TileTypeId.Amanita:
					return CreateAmanita(typeId, at);
			}

			throw new Exception($"Tile with type id {typeId} does not exist");
		}

		private GameEntity CreateAcorn(TileTypeId typeId, Vector3 at) =>
			CreateTileEntity(typeId, at)
				.With(x => x.isAcorn = true);

		private GameEntity CreateAmanita(TileTypeId typeId, Vector3 at) =>
			CreateTileEntity(typeId, at)
				.With(x => x.isAmanita = true);

		private GameEntity CreateTileEntity(TileTypeId typeId, Vector3 at)
		{
			TileConfig config = _staticDataService.GetTileConfig(typeId);
			Debug.Log(config);
			return CreateEntity.Empty()
					.AddId(_identifier.Next())
					.AddTileTypeId(typeId)
					.AddViewPrefab(config.PrefabView)
					.AddWorldPosition(at)
				;
		}
	}
}