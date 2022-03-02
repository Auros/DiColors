using DiColors.Daemons;
using DiColors.UI;
using Zenject;

namespace DiColors.Installers;

internal class DiColorsUIInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<UIDaemon>().AsSingle();
        Container.Bind<DiColorsView>().FromNewComponentAsViewController().AsSingle();
        Container.Bind<DiColorsTestingFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
    }
}