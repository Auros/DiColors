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
            Container.BindViewController<DiColorsInfoView>();
            Container.BindViewController<DiColorsMenuColorView>();
            Container.BindViewController<DiColorsGameColorView>();
            Container.BindFlowCoordinator<DiColorsFlowCoordinator>();
            Container.BindInterfacesTo<MenuButtonManager>().AsSingle();
        }
    }
}