using System;
using Zenject;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;

namespace DiColors.Services
{
	public class MenuButtonManager : IInitializable, IDisposable
	{
		private readonly MenuButton menuButton;
		private readonly MainFlowCoordinator _mainFlowCoordinator;
		private readonly DiColorsFlowCoordinator _diColorsFlowCoordinator;

		public MenuButtonManager(MainFlowCoordinator mainFlowCoordinator, DiColorsFlowCoordinator diColorsFlowCoordinator)
		{
			_mainFlowCoordinator = mainFlowCoordinator;
			_diColorsFlowCoordinator = diColorsFlowCoordinator;
			menuButton = new MenuButton("DiColors", SummonFlowCoordinator);
		}

		public void Initialize()
		{
			MenuButtons.instance.RegisterButton(menuButton);
		}

		public void Dispose()
		{
			MenuButtons.instance.UnregisterButton(menuButton);
		}

		private void SummonFlowCoordinator()
		{
			_mainFlowCoordinator.PresentFlowCoordinator(_diColorsFlowCoordinator);
		}
	}
}