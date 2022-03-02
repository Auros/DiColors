using DiColors.Coloring;
using DiColors.Services;
using Zenject;

namespace DiColors.Installers;

internal class DiColorsMenuColorInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<MenuTransformAccessor>().AsSingle();
        Container.Bind(typeof(TopSignColorizer), typeof(Colorizer)).To<TopSignColorizer>().AsSingle();
        Container.Bind<Colorizer>().To<BottomSignColorizer>().AsSingle();
    }
}