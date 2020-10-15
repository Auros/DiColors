using System;
using Zenject;
using System.Threading;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using DiColors.ViewControllers;
using BeatSaberMarkupLanguage.MenuButtons;

namespace DiColors.Services
{
	public class MenuButtonManager : IInitializable, IDisposable
	{
		private readonly MenuButton menuButton;
		private MainFlowCoordinator _mainFlowCoordinator;
		private DiColorsFlowCoordinator _diColorsFlowCoordinator;

		public MenuButtonManager(MainFlowCoordinator mainFlowCoordinator, DiColorsFlowCoordinator diColorsFlowCoordinator)
		{
			_mainFlowCoordinator = mainFlowCoordinator;
			_diColorsFlowCoordinator = diColorsFlowCoordinator;
			menuButton = new MenuButton("DiColors", SummonFlowCoordinator);
		}

		public async void Initialize()
		{
			await Task.Run(() => Thread.Sleep(100));
			MenuButtons.instance.RegisterButton(menuButton);
		}

		public void Dispose()
		{
			if (MenuButtons.IsSingletonAvailable)
			{
				MenuButtons.instance?.UnregisterButton(menuButton);
			}
		}

		private void SummonFlowCoordinator()
		{
			_mainFlowCoordinator.PresentFlowCoordinator(_diColorsFlowCoordinator);
		}
	}
}