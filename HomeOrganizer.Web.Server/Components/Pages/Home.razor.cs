using System.Threading.Tasks;
using System.Timers;
using HomeOrganizer.Logic;
using HomeOrganizer.Logic.Models;

namespace HomeOrganizer.Web.Server.Components.Pages;

public partial class Home
{
    private bool _showExplosion;
    private bool _isButtonExplosion;
    private Storage _storage;
    private IStorageEntry[] _itemsWithMissingCount;
    
    private readonly IStorageRepository _storageRepository 
        = new StorageRepository();

    private readonly IStorageManager _storageManager
        = new StorageManager();
    void LoadStorage()
    {
        _storage = _storageRepository.GetStorage("");
        _itemsWithMissingCount 
            = _storageManager
                .GetEntriesThatWeNeedToFillUpAgain(_storage);
    }
    async Task ShowExplosion()
    {
        _isButtonExplosion = true;
        _showExplosion = !_showExplosion;

        await Task.Delay(2000);
        _isButtonExplosion = false;
    }
}