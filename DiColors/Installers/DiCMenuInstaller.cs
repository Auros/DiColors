using System;
using Zenject;
using SiraUtil;
using DiColors.Services;
using DiColors.ViewControllers;

namespace DiColors.Installers
{
	public class DiCMenuInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			//Container.Bind<ProfileManager>().AsSingle();
			Container.Bind(typeof(IInitializable)).To<MenuColorSwapper>().AsSingle();
			Container.Bind(typeof(IInitializable), typeof(IDisposable)).To<MenuButtonManager>().AsSingle();

			// UI
			//Container.Bind<DiColorsProfileView>().FromNewComponentOnNewGameObject().AsSingle();
			Container.Bind<DiColorsInfoView>().FromNewComponentOnNewGameObject().AsSingle().OnInstantiated(Utilities.SetupViewController);
			Container.Bind<DiColorsMenuColorView>().FromNewComponentOnNewGameObject().AsSingle().OnInstantiated(Utilities.SetupViewController);
			Container.Bind<DiColorsFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
		}
	}
}