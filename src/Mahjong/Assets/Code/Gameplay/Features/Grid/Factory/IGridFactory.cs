namespace Code.Gameplay.Features.Grid.Factory
{
	public interface IGridFactory
	{
		GameEntity CreateGrid(GridTypeId layoutTypeId);
	}
}