using System.Threading.Tasks;
using _Scripts.Initialize;
using UnityEngine;
using Zenject;

namespace _Scripts.Services.EventsService
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
            Debug.Log($"::: FAKE_FIRE_BASE ::: {message}");
        }
    }
}