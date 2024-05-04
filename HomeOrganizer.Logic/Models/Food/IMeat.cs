namespace HomeOrganizer.Logic.Models.Food;

public interface IMeat : IStorageItem
{
    public MeatType MeatType { get; }
}