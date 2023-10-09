using _Scripts.AssetsProvider;
using _Scripts.GameEntities;
using UnityEngine;

namespace _Scripts.Factory
{
    [CreateAssetMenu]
    public class GameEntityFactory : AssetProvider
    {
        public GameEntity CreateGameEntity(string path, Transform parent)
        {
           var entity = CreateGameObject<GameEntity>(path, parent);
           return entity;
        }
    }
}