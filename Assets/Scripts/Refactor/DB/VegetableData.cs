[System.Serializable]
public class VegetableData
{
    public VegetableType vegetableType;
    public Vegetable.StateEnum growthState;

    public VegetableData(VegetableType type, Vegetable.StateEnum state)
    {
        vegetableType = type;
        growthState = state;
    }
}