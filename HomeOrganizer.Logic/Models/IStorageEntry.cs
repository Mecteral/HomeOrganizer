namespace HomeOrganizer.Logic.Models;

public interface IStorageEntry
{
    int PreferredCount { get; set; }
    int ActualCount { get; set; }
    IStorageItem StorageItem { get; set; }
}