public class BuildingSystemBuild
{
	private GridSystem _grid;

	public BuildingSystemBuild(GridSystem grid)
	{
		_grid = grid;
	}

	public void PlaceSelectedBuilding(Building selectBuilding, int placeX, int placeY)
	{
		Cell cornerCell = _grid.GetCell(placeX, placeY);
		cornerCell.Building = selectBuilding;
		for (int x = 0; x < selectBuilding.Size.x; x++)
		{
			for (int y = 0; y < selectBuilding.Size.y; y++)
			{
				_grid.GetCell(placeX + x, placeY + y).CellData.BuildingType = selectBuilding.BuildingType;
				_grid.GetCell(placeX + x, placeY + y).LinkedCell = cornerCell;
			}
		}
		selectBuilding.SetNormalColor();
		selectBuilding.transform.SetParent(_grid.GetCell(placeX, placeY).transform);

		SaveLoadSystem.AddBuilding(placeX, placeY, _grid.GetCell(placeX, placeY).CellData.BuildingType);
	}
}