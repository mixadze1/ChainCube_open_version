using System;
using _Scripts.PlayerComponents;
using UnityEngine;
using Zenject;

namespace _Scripts.SceneContext
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player _playerPrefab;    
    
        public override void InstallBindings()
        {
            RegisterPlayerDependecies();
        }

        private void RegisterPlayerDependecies()
        {
            Container.Bind<PlayerProgress>().AsSingle();
            Container.Bind<PlayerModel>().AsSingle().NonLazy();
            Container.Bind<PlayerMovement>().AsSingle();
            Container.Bind<IPlayer>().To<Player>().FromInstance(_playerPrefab);
        
        }
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