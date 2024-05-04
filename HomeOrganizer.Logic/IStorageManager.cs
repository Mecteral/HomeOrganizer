using HomeOrganizer.Logic.Models;

namespace HomeOrganizer.Logic;

public interface IStorageManager
{
    IStorageEntry[] GetEntriesThatWeNeedToFillUpAgain(Storage storage);
}