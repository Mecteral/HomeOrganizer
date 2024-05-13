using System;
using System.Collections.Generic;
using System.IO;
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
    private IStorageEntry[] _itemsWithMissingCount;
    private readonly FruitType[] _fruitTypes;
    private readonly IStorageManager _storageManager;
    private FruitType _selectedFruitType;
    private MeatType _selectedMeatType;
    private VegetableType _selectedVegetableType;
    private bool _isAddingFruit;
    private bool _isAddingMeat;
    private bool _isAddingVegetable;
    private int _actualCount;
    private int _preferredCount;
    private readonly VegetableType[] _vegetableTypes;
    private readonly MeatType[] _meatTypes;

    [Inject] 
    public IStorageRepository StorageRepository { get; set; }

    public Home()
    {
        _storageManager = new StorageManager();
        _fruitTypes = Enum.GetValues<FruitType>();
        _vegetableTypes = Enum.GetValues<VegetableType>();
        _meatTypes = Enum.GetValues<MeatType>();
    }

    async Task LoadStorage()
    {
        _isLoading = true;
        StateHasChanged();
       
        _storage = StorageRepository.GetStorage("");
        _itemsWithMissingCount 
            = _storageManager
                .GetEntriesThatWeNeedToFillUpAgain(_storage);
        _isLoading = false;
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
    }
}
