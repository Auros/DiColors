using Zenject;
using SiraUtil;
using DiColors.Services;
using DiColors.ViewControllers;

namespace DiColors.Installers
{
    public class DiCMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IInitializable), typeof(MenuColorSwapper)).To<MenuColorSwapper>().AsSingle();
            Container.Bind(typeof(IInitializable), typeof(SignColorSwapper)).To<SignColorSwapper>().AsSingle();

            // UI
            Container.Bind<DiColorsInfoView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<DiColorsMenuColorView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<DiColorsGameColorView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<DiColorsFlowCoordinator>().FromNewComponentOnNewGameObject(nameof(DiColorsFlowCoordinator)).AsSingle();
            Container.BindInterfacesTo<MenuButtonManager>().AsSingle();
        }
    }
}