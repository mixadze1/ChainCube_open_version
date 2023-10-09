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
        [SerializeField] private LoseWindow _loseWindow;
        [SerializeField] private Score _score;
        [SerializeField] private LoseHandler _loseHandler;
        [SerializeField] private GameEntityFactory _gameEntityFactory;
        [SerializeField] private ColorHandler _colorHandler;

        public JoystickInput JoystickInput;

        public override void InstallBindings()
        {
            BindInputService();
            BindGame();
            BindPresetsModels();
            BindGameEntityFactory();
            BindContainerGameEntityPositions();
            BindColorHandler();
            BindGameEntityTouchHandler();
            BindScoreView();
            BindSaveScore();
            BindLoseHandler();
            BindLoseWindow();
        }

        private void BindLoseWindow() => 
            Container.Bind<LoseWindow>().FromInstance(_loseWindow);

        private void BindLoseHandler() => 
            Container.Bind<LoseHandler>().FromInstance(_loseHandler);

        private void BindSaveScore() => 
            Container.Bind<SaveScore>().AsSingle().NonLazy();

        private void BindScoreView() => 
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

        private void BindGame()
        {
            Container.Bind<ILoseHandler>().To<Game.Game>().FromInstance(_game);
            Container.Bind<IGameHandler>().To<Game.Game>().FromInstance(_game);
        }

        private void BindInputService() => 
            Container.Bind<IInputService>().To<JoystickInput>().FromInstance(JoystickInput);
    }
}