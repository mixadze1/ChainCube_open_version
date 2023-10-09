using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Models
{
    [CreateAssetMenu(menuName = "EntityModel", fileName = "Preset")]
    public class PresetsGameModel : ScriptableObject
    {
        public List<GameEntityModel> GameEntityModelCollection;
    }
}