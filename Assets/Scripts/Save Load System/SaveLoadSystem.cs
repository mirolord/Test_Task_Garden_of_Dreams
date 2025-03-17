using System.IO;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public static class SaveLoadSystem
{
	private static List<CellData> buildingList = new List<CellData>();

	[System.Serializable]
	private class ArrayWrapper
	{
		public CellData[] BuildingData;
	}
	public static bool GridSaveExists => File.Exists(filePath);

	private static string filePath => Path.Combine(Application.dataPath, "buildingsData.json");

	public static void AddBuilding(int x, int y, BuildingType buildingType)
	{
		CellData building = new CellData(x, y, buildingType);
		if (building != null)
		{
			buildingList.Add(building);
			SaveData(buildingList.ToArray());
		}
	}

	public static void RemoveBuilding(int x, int y)
	{
		CellData buildingToRemove = buildingList.Find(building => building.X == x && building.Y == y);

		if (buildingToRemove != null)
		{
			buildingList.Remove(buildingToRemove);
			SaveData(buildingList.ToArray());
		}
		else
		{
			throw new System.NullReferenceException("Нет сохранённого здания.");
		}
	}
	public static CellData[] GetBuildings()
	{
		buildingList = LoadData().ToList();
		return buildingList.ToArray();
	}

	private static void SaveData(CellData[] buildings)
	{
		ArrayWrapper data = new ArrayWrapper { BuildingData = buildings ?? new CellData[0] };
		string json = JsonUtility.ToJson(data, true);
		File.WriteAllText(filePath, json);
	}

	private static CellData[] LoadData()
	{
		if (File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);
			ArrayWrapper wrapper = JsonUtility.FromJson<ArrayWrapper>(json);
			return wrapper?.BuildingData;
		}
		return null;
	}
}
