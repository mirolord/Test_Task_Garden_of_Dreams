using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
	[SerializeField] private GridSystem _grid;
	[SerializeField] private Building _buildingdPrefab;
	[SerializeField] private PlayerCursor _playerCursor;
	private Building _selectBuilding;

	private IBuildingSystemAction _action=null;
	private BuildingSystemRemove buildingSystemRemove;
	private BuildingSystemDragBuild buildingSystemBuild;

	private void Awake()
	{
		if (_grid == null)
			Debug.LogError("Не указан компонент grid.", this);
		if (_buildingdPrefab == null)
			Debug.LogError("Не указан компонент buildingdPrefab.", this);
		if (_playerCursor == null)
			Debug.LogError("Не указан компонент playerCursor.", this);
		_grid.CreateGrid();
        if (SaveLoadSystem.GridSaveExists)
			LoadBuildings();

		buildingSystemRemove = new BuildingSystemRemove(ref _grid);
		buildingSystemBuild = new BuildingSystemDragBuild(_grid);
	}

    private void LoadBuildings()
    {
        CellData[] BuildingData = SaveLoadSystem.GetBuildings();
        foreach (CellData cell in BuildingData)
        {
            LoadBuilding(cell.X, cell.Y, cell.BuildingType);
        }
    }

    public void EmptyBuildingMode()
    {
		SetPlacingBuilding(null);
		_action = null;
        _playerCursor.SetEmptyCursor();
    }

    public void PlaceBuildingMode(BuildingType buildingType)
	{
        Building building = InstantiateBuildingObject(buildingType);
		_playerCursor.SetEmptyCursor();
        SetPlacingBuilding(building);
		_action = buildingSystemBuild;
	}

	private Building InstantiateBuildingObject(BuildingType buildingType)
	{
		Building building = Instantiate(_buildingdPrefab);
		building.SetType(buildingType);
		return building;
	}

	public void LoadBuilding(int placeX, int placeY, BuildingType buildingType)
	{
		Building building = InstantiateBuildingObject(buildingType);
        building.transform.position = new Vector3(placeX, placeY, 0);
		building.transform.SetParent(_grid.GetCell(placeX, placeY).transform);
		Cell cornerCell = _grid.GetCell(placeX, placeY);
		for (int x = 0; x <= building.Size.x-1; x++)
		{
			for (int y = 0; y <= building.Size.y -1; y++)
			{
				_grid.GetCell(placeX + x, placeY + y).Building = building;
				_grid.GetCell(placeX + x, placeY + y).CellData.BuildingType = buildingType;
				_grid.GetCell(placeX + x, placeY + y).LinkedCell = cornerCell;
			}
		}
    }

	public void RemoveBuildingMode()
    {
		SetPlacingBuilding(null);
		_action = buildingSystemRemove;
		_playerCursor.SetRemoveCursor();
    }

	private void SetPlacingBuilding(Building building)
	{
		if (_selectBuilding != null)
		{
			Destroy(_selectBuilding.gameObject);
		}
		_selectBuilding = building;
	}

	private void Update()
	{
		if (_action != null)
		{
			bool isComplete = _action.Action(_selectBuilding);
			if (isComplete)
			{
				_selectBuilding = null;
				EmptyBuildingMode(); 
			}
		}
	}

}
