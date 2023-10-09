using System.Collections.Generic;
using _Scripts.GameEntities;
using _Scripts.Generators;
using _Scripts.Handlers;
using _Scripts.Models;
using _Scripts.Services.RandomService;
using UnityEngine;
using Zenject;

namespace _Scripts.Game
{
    public class Game : MonoBehaviour, IGameHandler, ILoseHandler
    {
        private List<PresetsGameModel> _presetsGame = new();

        private List<ContainerGameEntity> _containerGameEntity;

        private PresetsGameModel _presetsGameModel;
        private LoseWindow _loseWindow;
        private Score _score;

        private IRandomService _randomService;
        private IGeneratorEntity _generatorEntity;

        [Inject]
        private void Initialize(List<PresetsGameModel> presetsGameModel, IRandomService randomService,
            List<ContainerGameEntity> containerGameEntity, LoseWindow loseWindow,
            IGeneratorEntity generatorEntity, Score score)
        {
            _score = score;
            _generatorEntity = generatorEntity;
            _loseWindow = loseWindow;
            _presetsGame.AddRange(presetsGameModel);
            _containerGameEntity = containerGameEntity;
            _randomService = randomService;
            
            StartNewGame();
        }

        public void CompleteLose() => 
            RestartGame();

        private void RestartGame() => 
            StartNewGame();

        public void ActivateLoseWindow() => 
            _loseWindow.EnableView();

        public void CreateNewGameEntityAfterUsedPrevious() => 
            GenerateMainEntity();

        private void StartNewGame()
        {
            ScoreRestart();
            var presetGameModel = RandomPresetGameModel();
            var presetContainerPosition = RandomPresetContainerGameEntityWithPositions();
            GenerateEntity(presetGameModel, presetContainerPosition);
            GenerateMainEntity();
        }

        private void ScoreRestart() => 
            _score.Restart();

        private void GenerateMainEntity() => 
            _generatorEntity.GenerateMainEntity();

        private void GenerateEntity(PresetsGameModel presetsGameModel, ContainerGameEntity presetContainerPosition) => 
            _generatorEntity.GenerateEntity(presetsGameModel, presetContainerPosition);

        private PresetsGameModel RandomPresetGameModel() => 
            _presetsGame[GetRandomValue(0, _presetsGame.Count)];

        private ContainerGameEntity RandomPresetContainerGameEntityWithPositions() => 
            _containerGameEntity[GetRandomValue(0, _containerGameEntity.Count)];

        private int GetRandomValue(int minValue, int maxValue) => 
            _randomService.Next(minValue, maxValue);
    }
}