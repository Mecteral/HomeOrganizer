namespace HomeOrganizer.Logic.Models.Food;

public class Vegetables : IVegetables
{
    public Vegetables(VegetableType vegetableType)
    {
        VegetableType = vegetableType;
    }

    public VegetableType VegetableType { get; }
}