using _Scripts.PlayerComponents;
using UnityEngine;
using Zenject;

namespace _Scripts.Initialize
{
   public class Gamer : MonoBehaviour
   {
      private PlayerModel _player;

      [Inject]
      private void Initialize(PlayerModel player)
      {
         Debug.Log($"Player is : {_player}");
         _player = player;
      }
   }
}
