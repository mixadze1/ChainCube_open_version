using UnityEngine;

namespace _Scripts.PlayerComponents
{
    public class PlayerModel
    {
        private PlayerModel(IPlayer player)
        {
            Debug.Log($"PLayer :{player}");
        }
    }
}