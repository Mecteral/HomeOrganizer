using HomeOrganizer.Logic.Models;

namespace HomeOrganizer.Logic;

public interface IStorageRepository
{
    Storage GetStorage(string key);
}