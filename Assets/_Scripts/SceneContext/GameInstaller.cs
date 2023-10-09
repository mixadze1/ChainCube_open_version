using System.Collections.Generic;
using _Scripts.Factory;
using _Scripts.Game;
using _Scripts.GameEntities;
using _Scripts.Generators;
using _Scripts.Handlers;
using _Scripts.Models;
using _Scripts.ParentsTransform;
using _Scripts.Saves;
using _Scripts.Services.ClaculatorPowerTwo;
using _Scripts.Services.Input;
using UnityEngine;
using Zenject;

namespace _Scripts.SceneContext
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private List<PresetsGameModel> _presets;
        [SerializeField] private List<ContainerGameEntity> _containerGameEntity;

        [SerializeField] private Game.Game _game;
        [SerializeField] private LoseWindow _loseWindow;
        [SerializeField] private Score _score;
        [SerializeField] private ParentMainEntity _parentMain;
        [SerializeField] private ParentGameEntity _parentGameEntity;
        [SerializeField] private JoystickInput _joystickInput;
        
        [Header("Factory")]
        [SerializeField] private GameEntityFactory _gameEntityFactory;
        [SerializeField] private FactoryPoof _factoryPoof;
        
        [Header("Handlers")]
        [SerializeField] private ColorHandler _colorHandler;
        [SerializeField] private LoseHandler _loseHandler;

        public override void InstallBindings()
        {
            BindGame();
            BindInputService();
            BindPresetsModels();
            BindGameEntityFactory();
            BindPoofFactory();
            BindContainerGameEntityPositions();
            BindColorHandler();
            BindGameEntityTouchHandler();
            BindScore();
            BindSavesScore();
            BindLoseHandler();
            BindLoseWindow();
            BindGeneratorEntity();
            BindParents();
            BindCalculatorPowerTwoService();
        }

        private void BindPoofFactory() => 
            Container.Bind<FactoryPoof>().FromInstance(_factoryPoof);

        private void BindGame() => 
            Container.BindInterfacesTo<Game.Game>().FromInstance(_game);

        private void BindCalculatorPowerTwoService() => 
            Container.Bind<CalculatorPowerTwoService>().AsSingle();

        private void BindGeneratorEntity() => 
            Container.BindInterfacesTo<GenerateGameEntity>().AsSingle();

        private void BindLoseWindow() =>
            Container.Bind<LoseWindow>().FromInstance(_loseWindow).NonLazy();

        private void BindLoseHandler() =>
            Container.Bind<LoseHandler>().FromInstance(_loseHandler);

        private void BindSavesScore() =>
            Container.Bind<SaveScore>().AsSingle().NonLazy();

        private void BindScore() =>
            Container.Bind<Score>().FromInstance(_score);

        private void BindGameEntityTouchHandler() =>
            Container.Bind<GameEntityTouchHandler>().AsSingle();

        private void BindColorHandler() =>
            Container.Bind<ColorHandler>().FromInstance(_colorHandler);

        private void BindContainerGameEntityPositions() =>
            Container.Bind<List<ContainerGameEntity>>().FromInstance(_containerGameEntity);

        private void BindGameEntityFactory() =>
            Container.Bind<GameEntityFactory>().FromInstance(_gameEntityFactory);

        private void BindPresetsModels() =>
            Container.Bind<List<PresetsGameModel>>().FromInstance(_presets);

        private void BindInputService() =>
            Container.Bind<IInputService>().To<JoystickInput>().FromInstance(_joystickInput);

        private void BindParents()
        {
            Container.Bind<ParentGameEntity>().FromInstance(_parentGameEntity);
            Container.Bind<ParentMainEntity>().FromInstance(_parentMain);
        }
    }
}