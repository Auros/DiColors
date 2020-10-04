using HMUI;
using Zenject;
using BeatSaberMarkupLanguage;
using DiColors.ViewControllers;

namespace DiColors
{
	public class DiColorsFlowCoordinator : FlowCoordinator
	{
		private DiColorsInfoView _infoView;
		private MainFlowCoordinator _mainFlowCoordinator;

		[Inject]
		public void Construct(DiColorsInfoView infoView, MainFlowCoordinator mainFlowCoordinator)
		{
			_infoView = infoView;
			_mainFlowCoordinator = mainFlowCoordinator;
		}

		protected override void DidActivate(bool firstActivation, ActivationType activationType)
		{
			if (firstActivation)
			{
				title = "DiColors";
				showBackButton = true;
			}
			ProvideInitialViewControllers(_infoView);
		}

		protected override void BackButtonWasPressed(ViewController topViewController)
		{
			base.BackButtonWasPressed(topViewController);
			_mainFlowCoordinator.DismissFlowCoordinator(this);
		}
	}
}