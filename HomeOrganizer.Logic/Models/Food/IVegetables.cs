namespace HomeOrganizer.Logic.Models.Food;

public interface IVegetables : IStorageItem
{
    public VegetableType VegetableType { get; }
}