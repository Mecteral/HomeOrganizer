namespace HomeOrganizer.Logic.Models.Food;

public class Meat : IMeat
{
    public Meat(MeatType type)
    {
        MeatType = type;
    }

    public MeatType MeatType { get; }
}