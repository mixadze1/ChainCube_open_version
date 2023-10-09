using UnityEngine;

namespace _Scripts.AssetsProvider
{
    public class AssetProvider : ScriptableObject
    {
        protected static T CreateGameObject<T>(string path, Transform parent) where T : MonoBehaviour
        {
            var prefab = Resources.Load<T>(path);
            return Instantiate(prefab, parent);
        }
    }
}
