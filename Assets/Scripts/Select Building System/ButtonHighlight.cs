using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonHighlight : MonoBehaviour
{
	private Image _image;

	private void Awake()
	{
		_image = GetComponent<Image>();
	}

	public void TurnOn()
	{
		_image.enabled = true;
	}

	public void TurnOff()
	{
		_image.enabled = false;
	}
}
