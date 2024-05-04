namespace HomeOrganizer.Logic.Models.Food;

public class Fruit : IFruit
{
    public Fruit(FruitType fruitType)
    {
        FruitType = fruitType;
    }

    public FruitType FruitType { get; }
}