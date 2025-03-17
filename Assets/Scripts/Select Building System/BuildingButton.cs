using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BuildingButton : MonoBehaviour
{
	[SerializeField] private Image _image;
	[SerializeField] private ButtonHighlight _highlight; 
	private BuildingType _buildingType;
	public event System.Action<BuildingButton> OnBuildingSelected;

	private void Awake()
	{
		if (_highlight == null)
			Debug.LogError("Не указан компонент Highlight.", this);
	}

	public void SetBuildingType(BuildingType buildingType)
	{
		_buildingType= buildingType;
		_image.sprite = _buildingType.Sprite;
	}

	public BuildingType GetBuildingType() {
		if (_buildingType == null)
			throw new System.NullReferenceException("Не указан компонент BuildingType.");
		return _buildingType;
	} 

	public void OnClick()
	{
		OnBuildingSelected?.Invoke(this);
	}

	public void OnSelect()
	{
		_highlight.TurnOn();
	}

	public void OnDeSelect()
	{
		_highlight.TurnOff();
	}
}
