namespace HomeOrganizer.Logic.Models;

public abstract class AStorageEntry : IStorageEntry
{
    public int PreferredCount { get; set; }
    public int ActualCount { get; set; }
}