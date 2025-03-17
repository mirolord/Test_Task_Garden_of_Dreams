[System.Serializable]
public class CellData
{
    public CellData(int x, int y, BuildingType buildingType)
    {
        X = x;
        Y = y;
        BuildingType = buildingType;
    }

    public int X;
    public int Y;
    public BuildingType BuildingType;
}