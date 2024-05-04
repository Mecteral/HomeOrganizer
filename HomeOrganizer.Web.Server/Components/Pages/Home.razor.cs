using System;
using System.Threading.Tasks;
using HomeOrganizer.Logic;
using HomeOrganizer.Logic.Models;
using HomeOrganizer.Logic.Models.Food;

namespace HomeOrganizer.Web.Server.Components.Pages;

public partial class Home
{
    private bool _isLoading;
    private bool _showExplosion;
    private bool _isButtonExplosion;
    private Storage _storage;
    private IStorageEntry[] _itemsWithMissingCount;
    
    private readonly IStorageRepository _storageRepository 
        = new StorageRepository();

    private readonly IStorageManager _storageManager
        = new StorageManager();
    async Task LoadStorage()
    {
        _isLoading = true;
        StateHasChanged();
        await Task.Delay(2000);
        _storage = _storageRepository.GetStorage("");
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
}