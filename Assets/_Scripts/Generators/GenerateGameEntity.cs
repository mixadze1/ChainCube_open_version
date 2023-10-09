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
        private readonly FactoryPoof _poofFactory;

        private readonly IGameHandler _gameHandler;
        private readonly IInputService _inputService;

        private readonly List<GameEntity> _allGameEntities = new();

        private GenerateGameEntity(GameEntityFactory factoryGameEntity, GameEntityTouchHandler gameEntityTouchHandler,
            ColorHandler colorHandler, ParentGameEntity parentFromGameEntity, ParentMainEntity parentFromMainEntity,
            Score score,
            IGameHandler gameHandler, IInputService inputService, CalculatorPowerTwoService calculatorPowerTwoService, FactoryPoof poofFactory)
        {
            _poofFactory = poofFactory;
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

                var instance = CreateGameEntity(AssetPath.GameEntity, _parentFromGameEntity.transform);
                AddGameEntity(instance);

                var exponentValue = GetNumberForPowerOfTwo(entityModel);

                instance.Initialize(this, _gameEntityTouchHandler, exponentValue, _colorHandler,
                    presetContainerPosition.Positions[i].transform.position, _score, _poofFactory,
                    _parentFromGameEntity.transform);
                i++;
            }
        }

        public void GenerateMainEntity()
        {
            var instance = CreateGameEntity(AssetPath.GameEntityMain, _parentFromMainEntity.transform);

            instance.Initialize(this, _gameEntityTouchHandler, RandomValueEntityForMain(), _colorHandler,
                _parentFromMainEntity.transform.position, _score, _poofFactory, _parentFromGameEntity.transform);

            AddGameEntity(instance);

            var mainEntity = instance.GetComponent<GameEntityMain>();
            mainEntity.Initialize(instance, _parentFromGameEntity.transform, _inputService, gameHandler: _gameHandler);
        }

        private void AddGameEntity(GameEntity instance)
        {
            _allGameEntities.Add(instance);
        }

        public void ClearEntities()
        {
            foreach (var entity in _allGameEntities)
            {
                Object.Destroy(entity.gameObject);
            }
            _allGameEntities.Clear();
        }

        public void ReclaimEntity(GameEntity gameEntity)
        {
            _allGameEntities.Remove(gameEntity);
            Object.Destroy(gameEntity.gameObject);
        }

        private GameEntity CreateGameEntity(string path, Transform parentGameEntity) => 
            _factoryGameEntity.CreateGameEntity(path, parentGameEntity);

        private int GetNumberForPowerOfTwo(GameEntityModel entityModel) => 
            _calculatorPowerTwoService.GetNumberForPowerOfTwo(entityModel.ValueEntityPowerOfTwo);

        private int RandomValueEntityForMain() => 
            _calculatorPowerTwoService.RandomValueEntityForMain();
    }
}