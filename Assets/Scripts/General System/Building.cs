using UnityEngine;

public class Building: MonoBehaviour
{
	[SerializeField] private SpriteRenderer _renderer;
	private Color _normalColor;
	private int _normalLayer;
	public BuildingType BuildingType { get; private set; }
	public Vector2Int Size => BuildingType.BuildingSize;

	private void Awake()
	{
		if (_renderer == null)
			Debug.LogError("Не указан компонент renderer.", this);
		_normalColor = _renderer.material.color;
		_normalLayer = _renderer.sortingOrder;
	}

	public void SetType(BuildingType buildingType)
	{
		BuildingType = buildingType;
		_renderer.sprite = BuildingType.Sprite;
		_renderer.transform.position = new Vector2(Size.x-1, Size.y-1) / 2;
	}

	public void SetColor(bool available)
	{
		_renderer.material.color = available?Color.green:Color.red;
		_renderer.sortingOrder = _normalLayer+1;
	}

	public void SetNormalColor()
	{
		_renderer.sortingOrder = _normalLayer;
		_renderer.material.color = _normalColor;
	}
}