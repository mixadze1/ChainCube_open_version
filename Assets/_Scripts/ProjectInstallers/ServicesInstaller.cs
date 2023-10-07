using _Scripts.Ads;
using Zenject;

namespace _Scripts.Initialize
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            Container.Bind<IAdsService>().To<AdsService>().AsSingle().NonLazy();
            Container.Bind<GameSceneLoader>().AsSingle();
        }
    }
}