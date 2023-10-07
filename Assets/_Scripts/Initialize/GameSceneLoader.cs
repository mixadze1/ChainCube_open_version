using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Initialize
{
    public class GameSceneLoader
    {
        private bool _isLoadAdsService;

        private const string GameScene = "Game";

        public void CompleteAdsServices()
        {
            _isLoadAdsService = true;
            
            Debug.Log("CompleteAdsService");
            if (IsConditionNextScene())
                LoadGameScene();
        }

        private bool IsConditionNextScene()
        {
            return _isLoadAdsService;
        }

        private void LoadGameScene()
        {
            SceneManager.LoadScene(GameScene);
        }
    }
}