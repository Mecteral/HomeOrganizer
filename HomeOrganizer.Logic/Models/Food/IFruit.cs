namespace HomeOrganizer.Logic.Models.Food;

public interface IFruit : IStorageItem
{
    FruitType FruitType { get; }
}