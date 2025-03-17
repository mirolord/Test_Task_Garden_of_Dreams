using UnityEngine;

public class GridSystem : MonoBehaviour
{
	[SerializeField] private Vector2Int _createGridSize;
	[SerializeField] private Cell _cellPrefab;
	private readonly Color _orangeColor = new Color(1f, 0.49f, 0f);
	private Cell[,] _grid;

	public int rows => _grid?.GetLength(0) ?? throw new System.NullReferenceException("Не обнаружен компонент grid.");
	public int cols => _grid?.GetLength(1) ?? throw new System.NullReferenceException("Не обнаружен компонент grid.");

	private void Awake()
	{
		if (_cellPrefab == null)
			Debug.LogError("Не указан компонент cellPrefab.", this);
	}

	public void CreateGrid()
	{
		bool even;
		_grid = new Cell[_createGridSize.x, _createGridSize.y];
		Color evenColor = Color.magenta;
		Color oddColor = _orangeColor;

		for (int y = 0; y < _createGridSize.y; y++)
		{
			even = (y % 2 == 0);

			for (int x = 0; x < _createGridSize.x; x++)
			{
				Cell cell = Instantiate(_cellPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
				cell.CellData = new CellData(x, y, null);
				cell.SetColor(even ? evenColor : oddColor);
				cell.name = $"Cell {x} {y}";

				_grid[x, y] = cell;
				even = !even;
			}
		}
	}

	public Cell GetCell(int x, int y)
	{
		if (x < 0 || x >= rows || y < 0 || y >= cols)
		{
			Debug.LogWarning($"Запрошена некорректная ячейка: ({x}, {y})", this);
			return null;
		}	
		return _grid[x, y];
	}
}
