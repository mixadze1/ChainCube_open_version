using UnityEngine;

namespace _Scripts.Factory
{
    [CreateAssetMenu]
    public class GameEntityFactory : AssetProvider
    {
        public GameEntity.GameEntity CreateGameEntity(string path, Transform parent)
        {
           var entity = CreateGameObject<GameEntity.GameEntity>(path, parent);
           return entity;
        }
    }
}