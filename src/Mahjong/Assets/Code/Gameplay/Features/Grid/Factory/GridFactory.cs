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

		public GameEntity CreateGrid(GridTypeId id)
		{
			switch (id)
			{
				case GridTypeId.XSquare:
					return CreateXSquareGrid(id);
				case GridTypeId.FullRhombus:
					return CreateFullRhombusGrid(id);
				case GridTypeId.FullTriangle:
					return CreateFullTriangleGrid(id);
			}

			throw new Exception($"Grid with type id {id} does not exist");
		}

		private GameEntity CreateXSquareGrid(GridTypeId id) =>
			CreateGridEntity(id)
				.With(x => x.isSquareLayout= true)
				.With(x => x.isXMask =  true)
			;

		private GameEntity CreateFullRhombusGrid(GridTypeId id) =>
			CreateGridEntity(id)
				.With(x => x.isRhombusLayout = true)
				.With(x => x.isFullMask = true)
			;

		private GameEntity CreateFullTriangleGrid(GridTypeId id) =>
			CreateGridEntity(id)
				.With(x => x.isTriangleLayout = true)
				.With(x => x.isFullMask = true)
		;

		private GameEntity CreateGridEntity(GridTypeId id)
		{
			GridConfig config = _staticDataService.GetGridConfig(id);
			CellSize cellSize = config.CellSize;

			return CreateEntity.Empty()
				.AddId(_identifier.Next())
				.AddGridTypeId(config.Id)
				.AddCellPositions(new())
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