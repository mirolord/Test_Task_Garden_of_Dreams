using UnityEngine;

public class BuildingSystemRemove : IBuildingSystemAction
{
	private GridSystem _grid;
	private Camera _mainCamera;

	public BuildingSystemRemove(ref GridSystem grid)
	{
		_grid = grid;
		_mainCamera = Camera.main;
	}

	public bool Action(Building selectBuilding=null)
	{
		Plane groupPlane = new Plane(Vector3.forward, Vector3.zero);
		Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
		if (groupPlane.Raycast(ray, out float position))
		{
			Vector3 worldPosition = ray.GetPoint(position);
			int x = Mathf.RoundToInt(worldPosition.x);
			int y = Mathf.RoundToInt(worldPosition.y);

			if (x < 0 || x >= _grid.rows || y < 0 || y >= _grid.cols) return false;

			Cell cell = _grid.GetCell(x, y);
			if (cell == null || cell.CellData == null || cell.CellData.BuildingType == null) return false;

			if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			{
				Cell cornerCell = _grid.GetCell(x, y).LinkedCell;
				RemoveBuilding(cornerCell.CellData.X, cornerCell.CellData.Y);
				return true;
			}
		}
		return false;
	}


	private void RemoveBuilding(int placeX, int placeY)
	{
		Cell cell = _grid.GetCell(placeX, placeY);
		if (cell == null || cell.Building == null)
		{
			throw new System.NullReferenceException($"Не найдено здание в ({placeX}, {placeY})");
		}
		BuildingType buildingType = cell.CellData.BuildingType;
		Building building = _grid.GetCell(placeX, placeY).Building;
		if (building.gameObject == null)
			throw new System.NullReferenceException($"Не найден объект указанного здания в ({placeX}, {placeY})");
		Object.Destroy(building.gameObject);
		for (int x = 0; x < buildingType.BuildingSize.x; x++)
		{
			for (int y = 0; y < buildingType.BuildingSize.y; y++)
			{
				_grid.GetCell(placeX + x, placeY + y).CellData.BuildingType = null;
				_grid.GetCell(placeX + x, placeY + y).Building = null;
			}
		}

		SaveLoadSystem.RemoveBuilding(placeX, placeY);
	}
}
