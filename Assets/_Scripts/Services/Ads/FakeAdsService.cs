using System.Threading.Tasks;
using _Scripts.Initialize;
using UnityEngine;

namespace _Scripts.Services.Ads
{
    public class FakeAdsService : IAdsService
    {
        private readonly GameSceneLoader _gameSceneLoader;
        private bool _isConnectedToServer;
        
        private FakeAdsService(GameSceneLoader gameSceneLoader)
        {
            _gameSceneLoader = gameSceneLoader;
            InitializeServiceAds();
        }

        private async void InitializeServiceAds()
        {
            await InitializeServiceAdsAsync();
            _gameSceneLoader.CompleteAdsServices();
        }

        private async Task InitializeServiceAdsAsync()
        {
            await Task.Delay(250);
            Debug.Log("InitializeFakeAdsServiceAsync");
            _isConnectedToServer = true;
        }

        public bool TryShowAds() => 
            _isConnectedToServer;
    }
}