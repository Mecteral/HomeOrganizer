using System;
using System.IO;
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

    [Inject] 
    public IStorageRepository StorageRepository { get; set; }


    private readonly IStorageManager _storageManager;

    public Home()
    {
        _storageManager = new StorageManager();
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
}