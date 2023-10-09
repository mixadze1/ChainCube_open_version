using System;
using System.Collections.Generic;
using _Scripts.ProjectInstallers;
using _Scripts.Services.RandomService;

namespace _Scripts.Services.ClaculatorPowerTwo
{
    public class CalculatorPowerTwoService
    {
        private readonly IRandomService _randomService;

        private CalculatorPowerTwoService(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public int RandomValueEntityForMain()
        {
            var randomList = new List<int> { 2, 4, 8, 16 };
            var randomValue = _randomService.Next(0, randomList.Count);
            return randomList[randomValue];
        }

        public int GetNumberForPowerOfTwo(int exponent) => 
            (int)Math.Pow(2, exponent);
    }
}