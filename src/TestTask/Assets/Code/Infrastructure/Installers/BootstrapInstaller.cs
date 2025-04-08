using Code.Inventory;
using Code.Services.StaticDataService;
using Code.UI.Services.Factory;
using Code.UI.Services.Window;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindInfrastructureServices();
            BindGameplayServices();
            BindUIServices();
            BindUIFactories();
        }
        
        private void BindInfrastructureServices() => 
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();

        private void BindGameplayServices()
        {
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IInventoryService>().To<InventoryService>().AsSingle();
        }

        private void BindUIServices() => 
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();

        private void BindUIFactories() => 
            Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();

        public void Initialize() => 
            Container.Resolve<IStaticDataService>().LoadAll();
    }
}
