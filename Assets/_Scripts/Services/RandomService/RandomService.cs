using UnityEngine;

namespace _Scripts.Services.RandomService
{
    public class RandomService : IRandomService
    {
        public int Next(int min, int max) =>
            Random.Range(min, max);
    }
}