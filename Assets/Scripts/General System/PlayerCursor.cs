using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerCursor : MonoBehaviour
{
    [SerializeField] private Sprite _removeCursorSprite;
    private readonly Color _transparentColor = new Color(1f, 1f, 1f, 0.7f);
    private SpriteRenderer _spriteRenderer;

	private void Awake()
    {
        if (_removeCursorSprite == null)
            Debug.LogError("Не указан компонент Sprite.", this);
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

	private void Update()
	{
		Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		newPosition.z = 0;
		transform.position = newPosition;
	}

	public void SetEmptyCursor()
    {
        _spriteRenderer.sprite = null;
    }

    public void SetRemoveCursor()
    {
        _spriteRenderer.sprite = _removeCursorSprite;
        _spriteRenderer.color = _transparentColor;
    }
}