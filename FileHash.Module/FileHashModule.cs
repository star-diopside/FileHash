using FileHash.Module.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FileHash.Module
{
    public class FileHashModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainContent", typeof(MainView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
