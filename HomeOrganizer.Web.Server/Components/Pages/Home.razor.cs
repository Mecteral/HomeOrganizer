using System;
using System.Linq;
using System.Threading.Tasks;
using HomeOrganizer.Logic;
using HomeOrganizer.Logic.Models;
using HomeOrganizer.Logic.Models.Food;
using Microsoft.AspNetCore.Components;

namespace HomeOrganizer.Web.Server.Components.Pages;

public partial class Home
{
    private bool _isLoading;
    private bool _showExplosion;
    private bool _isButtonExplosion;
    private Storage _storage;
    private readonly IStorageManager _storageManager;
    private FruitType _selectedFruitType;
    private MeatType _selectedMeatType;
    private VegetableType _selectedVegetableType;
    private bool _isAddingFruit;
    private bool _isAddingMeat;
    private bool _isAddingVegetable;
    private int _actualCount;
    private int _preferredCount;
    private VegetableType[] _availableVegetableTypes;
    private MeatType[] _availableMeatTypes;
    private FruitType[] _availableFruitTypes;

    [Inject] 
    public IStorageRepository StorageRepository { get; set; }

    public Home()
    {
        _storageManager = new StorageManager();
        _availableFruitTypes = Enum.GetValues<FruitType>();
        _availableVegetableTypes = Enum.GetValues<VegetableType>();
        _availableMeatTypes = Enum.GetValues<MeatType>();
    }

    void LoadStorage()
    {
        var storage = StorageRepository.GetStorage("");
        _availableFruitTypes = Enum.GetValues<FruitType>()
            .Where(isFruitTypeStillAvailable)
            .ToArray();

        _availableMeatTypes = Enum.GetValues<MeatType>()
            .Where(isMeatTypeStillAvailable)
            .ToArray();

        _availableVegetableTypes = Enum.GetValues<VegetableType>()
            .Where(isVegetableTypeStillAvailable)
            .ToArray();

        _selectedVegetableType = _availableVegetableTypes.FirstOrDefault();
        _selectedMeatType = _availableMeatTypes.FirstOrDefault();
        _selectedFruitType = _availableFruitTypes.FirstOrDefault();

        _storage = storage;
        StateHasChanged();
        return;

        bool isFruitTypeStillAvailable(FruitType type) =>
            storage.StorageEntries
                .Select(e => e.StorageItem)
                .OfType<IFruit>()
                .All(fruit => fruit.FruitType != type);

        bool isMeatTypeStillAvailable(MeatType type) =>
            storage.StorageEntries
                .Select(e => e.StorageItem)
                .OfType<IMeat>()
                .All(meat => meat.MeatType != type);

        bool isVegetableTypeStillAvailable(VegetableType type) =>
            storage.StorageEntries
                .Select(e => e.StorageItem)
                .OfType<IVegetables>()
                .All(vegetables => vegetables.VegetableType != type);
    }

    async Task ShowExplosion()
    {
        _isButtonExplosion = true;
        _showExplosion = !_showExplosion;

        await Task.Delay(2000);
        _isButtonExplosion = false;
    }

    private string GetItemType(IStorageItem item) =>
        item switch
        {
            IFruit fruit => fruit.FruitType.ToString(),
            IMeat meat => meat.MeatType.ToString(),
            IVegetables vegetables => vegetables.VegetableType.ToString(),
            _ => throw new ArgumentOutOfRangeException(nameof(item))
        };


    private void Increase(IStorageEntry entry)
    {
        entry.ActualCount++;
        StorageRepository.SaveStorage(_storage);
    }

    private void Decrease(IStorageEntry entry)
    {
        entry.ActualCount--;
        StorageRepository.SaveStorage(_storage);
    }

    private void ShowAddFruit()
    {
        _isAddingFruit = true;
        _isAddingMeat = false;
        _isAddingVegetable = false;
    }

    private void ShowAddMeat()
    {
        _isAddingMeat = true;
        _isAddingVegetable = false;
        _isAddingFruit = false;
    }

    private void ShowAddVegetable()
    {
        _isAddingVegetable = true;
        _isAddingMeat = false;
        _isAddingFruit = false;
    }

    private void AddFruitEntry()
    {
        AddToEntries(new Fruit(_selectedFruitType));
    }

    private void AddVegetableEntry()
    {
        AddToEntries(new Vegetables(_selectedVegetableType));
    }

    private void AddMeatEntry()
    {
        AddToEntries(new Meat(_selectedMeatType));
    }

    private void AddToEntries(IStorageItem item)
    {
        var currentEntries = _storage.StorageEntries.ToList();
        currentEntries.Add(new StorageEntry
        {
            StorageItem = item,
            ActualCount = _actualCount,
            PreferredCount = _preferredCount
        });
        _storage.StorageEntries = currentEntries.ToArray();

        StorageRepository.SaveStorage(_storage);
        LoadStorage();
    }

    private bool HasMissingCount(IStorageEntry entry) 
        => entry.ActualCount < entry.PreferredCount;

    private void Remove(IStorageEntry entry)
    {
        var currentEntries = _storage.StorageEntries.ToList();
        currentEntries.Remove(entry);
        _storage.StorageEntries = currentEntries.ToArray();
        
        StorageRepository.SaveStorage(_storage);
        LoadStorage();
    }
}
