namespace HomeOrganizer.Logic.Models;

public interface IStorageEntry
{
    public int PreferredCount { get; set; }
    public int ActualCount { get; set; }
}