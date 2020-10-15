using Zenject;
using SiraUtil;
using DiColors.Services;
using BeatSaberMarkupLanguage;
using DiColors.ViewControllers;

namespace DiColors.Installers
{
	public class DiCMenuInstaller : Installer
	{
		public override void InstallBindings()
		{
			Container.Bind(typeof(IInitializable), typeof(MenuColorSwapper)).To<MenuColorSwapper>().AsSingle();

			// UI
			Container.BindViewController<DiColorsInfoView>(BeatSaberUI.CreateViewController<DiColorsInfoView>());
			Container.BindViewController<DiColorsMenuColorView>(BeatSaberUI.CreateViewController<DiColorsMenuColorView>());
			Container.BindViewController<DiColorsGameColorView>(BeatSaberUI.CreateViewController<DiColorsGameColorView>());
			Container.BindFlowCoordinator<DiColorsFlowCoordinator>(BeatSaberUI.CreateFlowCoordinator<DiColorsFlowCoordinator>());
			Container.BindInterfacesTo<MenuButtonManager>().AsSingle();

			//Container.Bind<DiColorsFlowCoordinator>().FromNewComponentOnNewGameObject(nameof(DiColorsFlowCoordinator)).AsSingle().Lazy();

		}
	}
}