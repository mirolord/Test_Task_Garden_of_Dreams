Для использования new Input System cоздать класс:

using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
	  [SerializeField] private InputAction _mousePosition;
	[SerializeField] private InputAction _leftMouseClick;

	public bool GetLeftButtonDown() => _leftMouseClick.triggered && _leftMouseClick.ReadValue<float>() > 0;
	public Vector2 GetMousePosition() => _mousePosition.ReadValue<Vector2>();

	public static InputHandler instance;

	private void Awake()
	{
		instance = this;
	}

	private void OnEnable()
	{
		_leftMouseClick.Enable();
		_mousePosition.Enable();
	}

	private void OnDisable()
	{
		_leftMouseClick.Disable();
		_mousePosition.Disable();
	}
}


Добавить ссылку на Unity.InputSystem в GeneralSystem.asmdef

В нужных местах указать ссылки на _inputHandler

Input.mousePosition		заменить на 	_inputHandler.GetMousePosition()
Input.GetMouseButtonDown(0)	заменить на	_inputHandler.GetLeftButtonDown()
