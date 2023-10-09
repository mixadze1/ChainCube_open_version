using System.Collections.Generic;
using _Scripts.Factory;
using _Scripts.GameEntity;
using _Scripts.ProjectInstallers;
using _Scripts.Services.Input;
using UnityEngine;
using Zenject;

namespace _Scripts.Game
{
    public class Game : MonoBehaviour, IGameHandler, ILoseHandler
    {
        [SerializeField] private Transform _gameEntityContainer;
        [SerializeField] private Transform _positionMainEntity;
        
        private List<PresetsGameModel> _presetsGame = new();

        private List<ContainerGameEntity> _containerGameEntiy;

        private List<GameEntity.GameEntity> _allGameEntities = new();
        
        private GameEntityFactory _factoryGameEntity;
        private PresetsGameModel _presetsGameModel;
        private ColorHandler _colorHandler;
        private GameEntityTouchHandler _gameEntityTouchHandler;
        private Score _score;

        private IRandomService _randomService;
        private IInputService _inputService;
        private LoseWindow _loseWindow;

        [Inject]
        private void Initialize(List<PresetsGameModel> presetsGameModel, ColorHandler colorHandler, IRandomService randomService,
            GameEntityFactory factoryGameEntity, List<ContainerGameEntity> containerGameEntity, GameEntityTouchHandler 
                gameEntityTouchHandler, IInputService inputService, Score score, LoseWindow loseWindow)
        {
            _loseWindow = loseWindow;
            _score = score;
            _inputService = inputService;
            _gameEntityTouchHandler = gameEntityTouchHandler;
            _presetsGame.AddRange(presetsGameModel);
            _colorHandler = colorHandler;
            _containerGameEntiy = containerGameEntity;
            _randomService = randomService;
            _factoryGameEntity = factoryGameEntity;
            GenerateGame();
        }

        public void CompleteLose() => 
            RestartGame();

        public void RestartGame()
        {
            foreach (var entity in _allGameEntities)
            {
                Destroy(entity.gameObject);
            }
            _allGameEntities.Clear();
            
            GenerateGame();
        }
        
        public void ActivateLoseWindow() => 
            _loseWindow.EnableView();

        public void CreateNewGameEntityAfterUsedPrevious() => 
            GenerateMainEntity();

        private void GenerateGame()
        {
            RandomPresets(_presetsGameModel,out var presetGameModel, out var presetContainerPosition);
            GenerateEntity(presetGameModel, presetContainerPosition);
            GenerateMainEntity();
        }

        private void GenerateMainEntity()
        {
            var instance = _factoryGameEntity.CreateGameEntity(AssetPath.GameEntityMain, _positionMainEntity);
            instance.Initialize(this, _gameEntityTouchHandler, RandomValueEntityForMain(), _colorHandler,
                _positionMainEntity.position, _score);
            _allGameEntities.Add(instance);
            var mainEntity = instance.GetComponent<GameEntityMain>();
            mainEntity.Initialize(instance, _gameEntityContainer, _inputService, gameHandler: this);
        }

        private int RandomValueEntityForMain()
        {
            List<int> randomList = new List<int> { 2, 4, 8, 16, };
            var randomValue = _randomService.Next(0, randomList.Count);
            return randomList[randomValue];
        }

        public void ReclaimGameEntity(GameEntity.GameEntity gameEntity)
        {
            _allGameEntities.Remove(gameEntity);
            Destroy(gameEntity.gameObject);
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
               _allGameEntities.Add(instance);
               instance.Initialize(this, _gameEntityTouchHandler, entityModel.ValueEntity, _colorHandler,
                   presetContainerPosition.Positions[i].transform.position, _score);
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
