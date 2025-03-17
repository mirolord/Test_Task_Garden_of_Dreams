using UnityEngine;

public class Panel : MonoBehaviour
{
	[SerializeField] private BuildingSelection _buildingPanel;
	[SerializeField] private BuildingSystem _builder;

	private void Awake()
	{
		if (_builder == null)
			Debug.LogError("Не указан компонент builder.", this);
		if (_buildingPanel == null)
			Debug.LogError("Не указан компонент buildingPanel.", this);
		_buildingPanel.Panel = this;
	}

	public void EmptyBuildingMode()
	{
		_builder.EmptyBuildingMode();
	}

	public void PlaceBuildingMode()
	{
		if (_buildingPanel.SelectedButton == null)
		{
			EmptyBuildingMode();
		}
		else
		{
			BuildingType selectedBuilding = _buildingPanel.SelectedButton.GetBuildingType();
			_builder.PlaceBuildingMode(selectedBuilding);
		}
	}

	public void RemoveBuildingMode()
	{
		_buildingPanel.DeselectBuilding();
		_builder.RemoveBuildingMode();
	}
}
