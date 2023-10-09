using System;
using UnityEngine.Serialization;

namespace _Scripts.Game
{
    [Serializable]
    public class GameEntityModel
    { 
        [FormerlySerializedAs("NumberEntity")] public int ValueEntity;

        private  int IncreaseValue = 2;
    
        public string Id;
    }
}