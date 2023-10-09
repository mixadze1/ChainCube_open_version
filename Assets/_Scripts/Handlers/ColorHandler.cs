using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Handlers
{
    [CreateAssetMenu]
    public class ColorHandler : ScriptableObject
    {
        [SerializeField] private List<Color> Colors; 
        
        public Color SetCurrentColor(int value)
        {
            var baseTwo = 2;
            var powerTwo = (int)Math.Log(value, baseTwo);
            return Colors[powerTwo];
        }
    }
}