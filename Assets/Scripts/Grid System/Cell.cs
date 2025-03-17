using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
	private SpriteRenderer _sprite;
	public CellData CellData { get; set; }
	public Cell LinkedCell { get; set; }
	public Building Building { get; set; }

	public void Awake()
	{
		_sprite = GetComponent<SpriteRenderer>();
	}

	public void SetColor(Color color)
	{
		_sprite.material.color = color;
	}
}