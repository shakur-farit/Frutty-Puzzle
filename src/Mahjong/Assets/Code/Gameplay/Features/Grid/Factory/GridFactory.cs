using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Grid.Config;
using Code.Infrastructure.Identifiers;
using Code.StaticData;

namespace Code.Gameplay.Features.Grid.Factory
{
	public class GridFactory : IGridFactory
	{
		private readonly IIdentifierService _identifier;
		private readonly IStaticDataService _staticDataService;

		public GridFactory(IIdentifierService identifier, IStaticDataService staticDataService)
		{
			_identifier = identifier;
			_staticDataService = staticDataService;
		}

		public GameEntity CreateGrid(GridTypeId typeId)
		{
			switch (typeId)
			{
				case GridTypeId.Square:
					return CreateSquareGrid(typeId);
				case GridTypeId.Rhombus:
					return CreateRhombusGrid(typeId);
			}

			throw new Exception($"Grid with type id {typeId} does not exist");
		}

		private GameEntity CreateSquareGrid(GridTypeId typeId) =>
			CreateGridEntity(typeId)
				.With(x => x.isSquare = true);

		private GameEntity CreateRhombusGrid(GridTypeId typeId) =>
			CreateGridEntity(typeId)
				.With(x => x.isRhombus = true);

		private GameEntity CreateGridEntity(GridTypeId typeId)
		{
			GridConfig config = _staticDataService.GetGridConfig(typeId);
			CellSize cellSize = config.CellSize;

			return CreateEntity.Empty()
				.AddId(_identifier.Next())
				.AddGridTypeId(config.Id)
				.AddGridColumns(config.GridColumns)
				.AddGridRows(config.GridRows)
				.AddGridLayers(config.GridLayers)
				.AddCellSizeX(cellSize.CellSizeX)
				.AddCellSizeY(cellSize.CellSizeY)
				.AddCellSizeZ(cellSize.CellSizeZ)
				;
		}
	}
}