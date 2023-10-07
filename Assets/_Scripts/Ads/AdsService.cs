using System.Threading.Tasks;
using _Scripts.Initialize;
using UnityEngine;

namespace _Scripts.Ads
{
    public class AdsService : IAdsService
    {
        private readonly GameSceneLoader _gameSceneLoader;
        private bool _isConnectedToServer;
        
        private AdsService(GameSceneLoader gameSceneLoader)
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
            Debug.Log("InitializeServiceAdsAsync");
            await Task.Delay(250);
            _isConnectedToServer = true;
        }

        public bool TryShowAds() => 
            _isConnectedToServer;
    }
}