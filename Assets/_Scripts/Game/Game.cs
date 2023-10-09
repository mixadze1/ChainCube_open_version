using System;
using System.Collections.Generic;
using _Scripts.Factory;
using _Scripts.GameEntity;
using _Scripts.ProjectInstallers;
using _Scripts.Services.Input;
using UnityEngine;
using Zenject;

namespace _Scripts.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Transform _gameEntityContainer;
        [SerializeField] private Transform _positionMainEntity;
        
        private List<PresetsGameModel> _presetsGame = new();

        private List<ContainerGameEntity> _containerGameEntiy;
        
        private GameEntityFactory _factoryGameEntity;
        private PresetsGameModel _presetsGameModel;
        private ColorHandler _colorHandler;
        private GameEntityTouchHandler _gameEntityTouchHandler;

        private IRandomService _randomService;
        private IInputService _inputService;

        [Inject]
        private void Initialize(List<PresetsGameModel> presetsGameModel, ColorHandler colorHandler, IRandomService randomService,
            GameEntityFactory factoryGameEntity, List<ContainerGameEntity> containerGameEntity, GameEntityTouchHandler gameEntityTouchHandler, IInputService inputService)
        {
            _inputService = inputService;
            _gameEntityTouchHandler = gameEntityTouchHandler;
            _presetsGame.AddRange(presetsGameModel);
            _colorHandler = colorHandler;
            _containerGameEntiy = containerGameEntity;
            _randomService = randomService;
            _factoryGameEntity = factoryGameEntity;
            GenerateGame();
        }

        private void GenerateGame()
        {
            RandomPresets(_presetsGameModel,out var presetGameModel, out var presetContainerPosition);
            GenerateEntity(presetGameModel, presetContainerPosition);
            GenerateMainEntity(presetGameModel,  presetContainerPosition);
        }

        private void GenerateMainEntity(PresetsGameModel presetsGameModel, ContainerGameEntity presetContainerPosition)
        {
           var instance = _factoryGameEntity.CreateGameEntity(AssetPath.GameEntityMain, _positionMainEntity);
           instance.Initialize(_gameEntityTouchHandler, presetsGameModel.MainEntityModel, _colorHandler, _positionMainEntity.position);
            var mainEntity = instance.GetComponent<GameEntityMain>();
            mainEntity.Initialize( instance, presetContainerPosition.transform, _inputService);
        }

        private void RandomPresets(PresetsGameModel presetsGameModel, out PresetsGameModel presetGameModel, out ContainerGameEntity  presetContainerPosition)
        {
            var valueModel  = GetRandomValue(0, _presetsGame.Count);
            presetGameModel = SetPresetModel(valueModel);
            
            var valuePosition = GetRandomValue(0, _containerGameEntiy.Count);
             presetContainerPosition = SetPresetPosition(valuePosition);
        }

        private void GenerateEntity(PresetsGameModel presetsGameModel, ContainerGameEntity presetContainerPosition)
        {
            var i = 0;
            foreach (var entityModel in presetsGameModel.GameEntityModelCollection)
            {
               var instance =  _factoryGameEntity.CreateGameEntity(AssetPath.GameEntity, _gameEntityContainer);
               instance.Initialize(_gameEntityTouchHandler, entityModel, _colorHandler, presetContainerPosition.Positions[i].transform.position);
               i++;
            }
        }

        private int GetRandomValue(int minValue, int maxValue) => 
            _randomService.Next(minValue, maxValue);

        private ContainerGameEntity SetPresetPosition(int valuePosition) => 
            _containerGameEntiy[valuePosition];

        private PresetsGameModel SetPresetModel(int value) => 
            _presetsGame[value];
    }
}
