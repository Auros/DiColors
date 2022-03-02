using DiColors.Coloring;
using DiColors.Daemons;
using DiColors.Services;
using Zenject;

namespace DiColors.Installers;

internal class DiColorsMenuColorInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<MenuTransformAccessor>().AsSingle();
        Container.Bind<Colorizer>().To<TopSignColorizer>().AsSingle();
        Container.Bind<Colorizer>().To<BottomSignColorizer>().AsSingle();
        Container.BindInterfacesTo<StartupColoringDaemon>().AsSingle();
    }
}