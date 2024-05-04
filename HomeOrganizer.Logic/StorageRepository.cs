using HomeOrganizer.Logic.Models;
using HomeOrganizer.Logic.Models.Food;

namespace HomeOrganizer.Logic;

public class StorageRepository : IStorageRepository
{
    public Storage GetStorage(string key)
    {
        return new Storage("First")
        {
            StorageEntries = new IStorageEntry[]
            {
                new StorageEntry()
                {
                    PreferredCount = 13,
                    ActualCount = 7,
                    StorageItem = new Meat(MeatType.Chop)
                },
                new StorageEntry()
                {
                    PreferredCount = 200,
                    ActualCount = 218,
                    StorageItem = new Meat(MeatType.Steak)
                },
                new StorageEntry()
                {
                    PreferredCount = 1,
                    ActualCount = 2,
                    StorageItem = new Vegetables(VegetableType.Carrot)
                },
                new StorageEntry()
                {
                    PreferredCount = 8,
                    ActualCount = 700185,
                    StorageItem = new Fruit(FruitType.Grapefruit)
                }
            }
        };
    }
}