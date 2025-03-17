using UnityEngine;
[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Buildings", order = 51)]
public class BuildingType :ScriptableObject
{
	[SerializeField] private Sprite _sprite;
	public Vector2Int BuildingSize;
	public Sprite Sprite => _sprite;
}