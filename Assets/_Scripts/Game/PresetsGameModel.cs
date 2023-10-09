using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Game
{
    [CreateAssetMenu(menuName = "EntityModel")]
    public class PresetsGameModel : ScriptableObject
    {
        [FormerlySerializedAs("GameEntityModel")] public List<GameEntityModel> GameEntityModelCollection;
        public GameEntityModel MainEntityModel;
    }
}