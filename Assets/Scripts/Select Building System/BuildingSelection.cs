using System.Collections.Generic;
using UnityEngine;

public class BuildingSelection : MonoBehaviour
{
	[SerializeField] private BuildingButton _buildingButtonPrefab;
	[SerializeField] private List<BuildingType> _buildingTypes;
	public Panel Panel { private get; set; }
	public BuildingButton SelectedButton { get; private set; }

	private void Awake()
	{
		if (_buildingButtonPrefab == null)
			Debug.LogError("Не указан компонент buildingButtonPrefab.", this);
		if (_buildingTypes == null || _buildingTypes.Count == 0)
		{
			Debug.LogError("Список buildingTypes пуст или не задан.");
		}

		foreach (BuildingType buildingType in _buildingTypes)
		{
			BuildingButton newButton = Instantiate(_buildingButtonPrefab, transform);
			newButton.SetBuildingType(buildingType);
			newButton.OnBuildingSelected += SelectBuilding;
		}
	}

	public void DeselectBuilding()
	{
		SelectedButton?.OnDeSelect();
		SelectedButton = null;
	}

	public void SelectBuilding(BuildingButton buildingButton)
	{
		DeselectBuilding();
		SelectedButton = buildingButton;
		SelectedButton.OnSelect();
		Panel.EmptyBuildingMode();
	}
}
