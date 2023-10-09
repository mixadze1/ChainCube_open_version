using _Scripts.Ads;
using _Scripts.Initialize;
using _Scripts.Services.Input;
using Zenject;

namespace _Scripts.ProjectInstallers
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            RegisterServices();

        private void RegisterServices()
        {
            BindGameSceneLoader();
            BindAdsService();
            BindLogEventsService();
            BindRandomService();
        }

        private void BindRandomService() => 
            Container.Bind<IRandomService>().To<RandomService>().AsSingle();

        private void BindLogEventsService() => 
            Container.Bind<ILogEventsService>().To<FakeFireBaseService>().AsSingle().NonLazy();

        private void BindAdsService() => 
            Container.Bind<IAdsService>().To<AdsService>().AsSingle().NonLazy();

        private void BindGameSceneLoader() => 
            Container.Bind<GameSceneLoader>().AsSingle();
    }
}