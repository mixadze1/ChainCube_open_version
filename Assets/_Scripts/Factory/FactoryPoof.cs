using _Scripts.AssetsProvider;
using _Scripts.GameEntities;
using UnityEngine;

namespace _Scripts.Factory
{
    [CreateAssetMenu(menuName = "Factory/Poof", fileName = "PoofFactory")]
    public class FactoryPoof : AssetProvider
    {
        private float _scale = 3;
        public Poof CreatePoof(string path, Transform parent)
        {
            var entity = CreateGameObject<Poof>(path, parent);
            entity.transform.localScale = Vector3.one *  _scale;
            return entity;
        }
    }
}