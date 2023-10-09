using System.Collections.Generic;
using _Scripts.AssetsProvider;
using _Scripts.Factory;
using _Scripts.Game;
using _Scripts.GameEntities;
using _Scripts.Handlers;
using _Scripts.Models;
using _Scripts.ParentsTransform;
using _Scripts.Services.ClaculatorPowerTwo;
using _Scripts.Services.Input;
using UnityEngine;

namespace _Scripts.Generators
{
    public class GenerateGameEntity : IGeneratorEntity, IReclaimerEntity
    {
        private readonly GameEntityFactory _factoryGameEntity;
        private readonly GameEntityTouchHandler _gameEntityTouchHandler;
        private readonly ColorHandler _colorHandler;
        private readonly ParentGameEntity _parentFromGameEntity;
        private readonly ParentMainEntity _parentFromMainEntity;
        private readonly Score _score;
        private readonly CalculatorPowerTwoService _calculatorPowerTwoService;

        private readonly IGameHandler _gameHandler;
        private readonly IInputService _inputService;

        private readonly List<GameEntity> _allGameEntities = new();

        private GenerateGameEntity(GameEntityFactory factoryGameEntity, GameEntityTouchHandler gameEntityTouchHandler,
            ColorHandler colorHandler, ParentGameEntity parentFromGameEntity, ParentMainEntity parentFromMainEntity,
            Score score,
            IGameHandler gameHandler, IInputService inputService, CalculatorPowerTwoService calculatorPowerTwoService)
        {
            _calculatorPowerTwoService = calculatorPowerTwoService;
            _factoryGameEntity = factoryGameEntity;
            _gameEntityTouchHandler = gameEntityTouchHandler;
            _colorHandler = colorHandler;
            _parentFromGameEntity = parentFromGameEntity;
            _parentFromMainEntity = parentFromMainEntity;
            _score = score;
            _gameHandler = gameHandler;
            _inputService = inputService;
        }

        public void GenerateEntity(PresetsGameModel presetsGameModel, ContainerGameEntity presetContainerPosition)
        {
            var i = 0;
            foreach (var entityModel in presetsGameModel.GameEntityModelCollection)
            {
                if (i >= presetContainerPosition.Positions.Count)
                {
                    Debug.LogError("Not have Position for Spawn! Add positions!");
                    return;
                }
                
                var instance = _factoryGameEntity.CreateGameEntity(AssetPath.GameEntity, _parentFromGameEntity.transform);
                _allGameEntities.Add(instance);
                
                var exponentValue =  GetNumberForPowerOfTwo(entityModel);
                
                instance.Initialize(this, _gameEntityTouchHandler, exponentValue, _colorHandler,
                    presetContainerPosition.Positions[i].transform.position, _score);
                i++;
            }
        }

        public void GenerateMainEntity()
        {
            var instance =
                _factoryGameEntity.CreateGameEntity(AssetPath.GameEntityMain, _parentFromMainEntity.transform);
            instance.Initialize(this, _gameEntityTouchHandler, RandomValueEntityForMain(), _colorHandler,
                _parentFromMainEntity.transform.position, _score);
            _allGameEntities.Add(instance);
            var mainEntity = instance.GetComponent<GameEntityMain>();
            mainEntity.Initialize(instance, _parentFromGameEntity.transform, _inputService, gameHandler: _gameHandler);
        }

        private int GetNumberForPowerOfTwo(GameEntityModel entityModel) => 
            _calculatorPowerTwoService.GetNumberForPowerOfTwo(entityModel.ValueEntityPowerOfTwo);

        private int RandomValueEntityForMain() => 
            _calculatorPowerTwoService.RandomValueEntityForMain();

        public void ReclaimEntity(GameEntity gameEntity)
        {
            _allGameEntities.Remove(gameEntity);
            Object.Destroy(gameEntity.gameObject);
        }

        public void ClearEntities()
        {
            foreach (var entity in _allGameEntities)
            {
                Object.Destroy(entity.gameObject);
            }
            _allGameEntities.Clear();
        }
    }
}