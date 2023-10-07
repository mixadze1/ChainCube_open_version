using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Scripts.PlayerComponents
{
    public class Player : MonoBehaviour, IPlayer
    {
        [Inject]
        private void Initialize(PlayerMovement playerMovement, PlayerModel playerModel)
        {
            Debug.Log($" playerModel : {playerModel} { playerMovement}");
            NextLevelLoad();
        }

        private void NextLevelLoad() => 
            SceneManager.LoadScene("Game");
    }
}