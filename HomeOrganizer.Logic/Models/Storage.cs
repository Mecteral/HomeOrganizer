namespace HomeOrganizer.Logic.Models;

public class Storage
{
    public Storage(string key)
    {
        Key = key;
    }

    public string Key { get; }
    public IStorageEntry[] StorageEntries { get; set; }
}