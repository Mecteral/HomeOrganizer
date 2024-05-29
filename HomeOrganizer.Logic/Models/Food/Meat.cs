namespace HomeOrganizer.Logic.Models.Food;

public class Meat : IMeat
{
    public Meat(MeatType meatType)
    {
        MeatType = meatType;
    }

    public MeatType MeatType { get; }
}