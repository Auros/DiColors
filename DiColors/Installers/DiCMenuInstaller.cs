using System;
using Zenject;
using DiColors.Services;
using DiColors.ViewControllers;

namespace DiColors.Installers
{
	public class DiCMenuInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind(typeof(IInitializable)).To<MenuColorSwapper>().AsSingle();
			Container.Bind(typeof(IInitializable), typeof(IDisposable)).To<MenuButtonManager>().AsSingle();

			// UI
			Container.Bind<DiColorsInfoView>().FromNewComponentOnRoot().AsSingle();
			Container.Bind<DiColorsFlowCoordinator>().FromNewComponentOnRoot().AsSingle();
		}
	}
}