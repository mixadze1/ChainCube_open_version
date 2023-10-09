using UnityEngine;

namespace _Scripts.ProjectInstallers
{
    public class RandomService : IRandomService
    {
        public int Next(int min, int max) =>
            Random.Range(min, max);
    }
}