using System.Collections.Generic;
using HomeOrganizer.Logic.Models;

namespace HomeOrganizer.Logic;

public class StorageManager : IStorageManager
{
    public IStorageEntry[] GetEntriesThatWeNeedToFillUpAgain(Storage storage)
    {
        var result = new List<IStorageEntry>();
        foreach (var entry in storage.StorageEntries)
        {
            if (entry.ActualCount < entry.PreferredCount)
                result.Add(entry);
        }
        return result.ToArray();
    }
}