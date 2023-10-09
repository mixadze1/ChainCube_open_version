using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Initialize
{
    public class GameSceneLoader
    {
        private bool _isLoadAdsService;
        private bool _isLoadEventsService;
        
        private const string GameScene = "Game";

        public void CompleteAdsServices()
        {
            _isLoadAdsService = true;
            
            Debug.Log("CompleteAdsService");
            if (IsConditionNextScene())
                LoadGameScene();
        }

        public void CompleteEventsService()
        {
            _isLoadEventsService = true;
            
            if(IsConditionNextScene())
                LoadGameScene();
        }
        

        private bool IsConditionNextScene()
        {
            return _isLoadAdsService && _isLoadEventsService;
        }

        private void LoadGameScene()
        {
            Debug.Log("Load Game Scene");
            SceneManager.LoadScene(GameScene);
        }
    }
}