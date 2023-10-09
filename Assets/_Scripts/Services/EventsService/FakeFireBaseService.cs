using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Scripts.Initialize
{
    public class FakeFireBaseService : ILogEventsService
    {
        [Inject]
        private async Task InitializeFakeFireBaseAsync(GameSceneLoader gameSceneLoader)
        {
            await Task.Delay(100);
            
            Debug.Log("Initialize Fake Fire Base!");
            
            gameSceneLoader.CompleteEventsService();
        }

        public void LogEvents(string message)
        {
            Debug.Log($"::: MESSAGE ::: {message}");
        }
        
        
        
    }
}