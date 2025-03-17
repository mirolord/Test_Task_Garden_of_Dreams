using UnityEngine;

public class BuildingSystemDragBuild : IBuildingSystemAction
{
	private GridSystem _grid;
	private Camera _mainCamera;
	private BuildingSystemBuild buildingsystemBuild;

	public BuildingSystemDragBuild(GridSystem grid)
	{
		buildingsystemBuild = new BuildingSystemBuild(grid);
		_grid = grid;
		_mainCamera = Camera.main;
	}

	public bool Action(Building selectBuilding)
	{
		if (selectBuilding == null)
			throw new System.NullReferenceException("Не выбрано здание.");
		Plane groupPlane = new Plane(Vector3.forward, Vector3.zero);
		Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
		if (groupPlane.Raycast(ray, out float position))
		{
			Vector3 worldPosition = ray.GetPoint(position);
			int x = Mathf.RoundToInt(worldPosition.x);
			int y = Mathf.RoundToInt(worldPosition.y);

			bool available = true;

			if (x < 0 || x > _grid.rows - selectBuilding.Size.x) available = false;
			if (y < 0 || y > _grid.cols - selectBuilding.Size.y) available = false;

			if (available && IsPlaceTaken(x, y, selectBuilding.Size.x, selectBuilding.Size.y)) available = false;

			selectBuilding.transform.position = new Vector3(x, y, 0);
			selectBuilding.SetColor(available);
		
			if (available)
			{
				if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
				{
					buildingsystemBuild.PlaceSelectedBuilding(selectBuilding, x, y);
					return true;
				}
			}
		}
		return false;
	}

	private bool IsPlaceTaken(int placeX, int placeY, int sizeX, int sizeY)
	{
		for (int x = 0; x < sizeX; x++)
		{
			for (int y = 0; y < sizeY; y++)
			{
				if (_grid.GetCell(placeX + x, placeY + y).CellData.BuildingType != null) return true;
			}
		}
		return false;
	}
}