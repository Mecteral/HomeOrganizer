using System;
using System.IO;
using System.Linq;
using HomeOrganizer.Logic.Models;
using HomeOrganizer.Logic.Models.Food;
using Newtonsoft.Json;

namespace HomeOrganizer.Logic;

public class StorageRepository : IStorageRepository
{
    private readonly string _filePath;
    public StorageRepository()
    {
        var userPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var folderPath = Path.Combine(userPath, "HomeOrganizer");
        _filePath = Path.Combine(folderPath, "Storages.json");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
        if (!File.Exists(_filePath))
            File.Create(_filePath);
    }

    public Storage GetStorage(string key)
    {
        var storages = GetStorages();
        foreach (var storage in storages)
        {
            if (storage.Key == key)
                return storage;
        }
        // Default returns if no storage was found
        var result =  new Storage(key)
        {
            StorageEntries = new IStorageEntry[]
            {
                new StorageEntry()
                {
                    StorageItem = new Fruit(FruitType.Grapefruit),
                    ActualCount = 0,
                    PreferredCount = 0
                }
            }
        };
        SaveStorage(result);
        return result;
    }

    public Storage[] GetStorages()
    {
        var storageJson = File.ReadAllText(_filePath);
        if (string.IsNullOrWhiteSpace(storageJson))
            return new Storage[]
            {
                
            };

        return JsonConvert.DeserializeObject<Storage[]>(storageJson, new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Objects
        });
    }

    public void SaveStorage(Storage storage)
    {
        var storages = GetStorages();
        Storage storageToSave = null;
        foreach (var savedStorage in storages)
        {
            if (savedStorage.Key == storage.Key)
            {
                storageToSave = savedStorage;
                storageToSave.StorageEntries = storage.StorageEntries;
            }
        }

        if (storageToSave == null)
        {
            var storageList = storages.ToList();
            storageList.Add(storage);
            storages = storageList.ToArray();
        }
        var text = JsonConvert.SerializeObject(storages, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Objects,
            Formatting = Formatting.Indented
        });
        File.WriteAllText(_filePath, text);
    }
}