using DiColors.Coloring;
using Zenject;

namespace DiColors.Installers;

internal class DiColorsMenuColorInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(TopSignColorizer), typeof(Colorizer)).To<TopSignColorizer>().AsSingle();
    }
}