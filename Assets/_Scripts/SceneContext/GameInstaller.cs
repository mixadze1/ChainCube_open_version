using System;
using System.Collections.Generic;
using _Scripts.Factory;
using _Scripts.Game;
using _Scripts.GameEntity;
using _Scripts.Services.Input;
using UnityEngine;
using Zenject;

namespace _Scripts.SceneContext
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private List<PresetsGameModel> _presets;
        [SerializeField] private List< ContainerGameEntity> _containerGameEntity;
        
        [SerializeField] private Game.Game _game;

        [SerializeField] private GameEntityFactory _gameEntityFactory;

        [SerializeField] private ColorHandler _colorHandler;

        public JoystickInput JoystickInput;

        public override void InstallBindings()
        {
            RegisterPlayerDependecies();
        }

        private void RegisterPlayerDependecies()
        {
            BindInputService();
            BindPlayer();
            BindPresetsGame();
            BindGameEntityFactory();
            BindContainerGameEntityPositions();
            BindColorHandler();
            BindGameEntityTouchHandler();
        }

        private void BindGameEntityTouchHandler()
        {
            Container.Bind<GameEntityTouchHandler>().AsSingle();
        }

        private void BindColorHandler() => 
            Container.Bind<ColorHandler>().FromInstance(_colorHandler);

        private void BindContainerGameEntityPositions() => 
            Container.Bind<List<ContainerGameEntity>>().FromInstance(_containerGameEntity);

        private void BindGameEntityFactory() => 
            Container.Bind<GameEntityFactory>().FromInstance(_gameEntityFactory);

        private void BindPresetsGame() => 
            Container.Bind<List<PresetsGameModel>>().FromInstance(_presets);

        private void BindPlayer() => 
            Container.Bind<Game.Game>().FromInstance(_game);

        private void BindInputService() => 
            Container.Bind<IInputService>().To<JoystickInput>().FromInstance(JoystickInput);
    }

    [Serializable]
    public class PlayerProgress
    {
        public MyVector3 MyVector3;
    }

    public class MyVector3
    {
        public float X;
        public float Y;
        public float Z;
    }
}