using HMUI;
using Zenject;
using BeatSaberMarkupLanguage;
using DiColors.ViewControllers;

namespace DiColors
{
	public class DiColorsFlowCoordinator : FlowCoordinator
	{
		private DiColorsInfoView _infoView;
		//private DiColorsProfileView _profileView;
		private MainFlowCoordinator _mainFlowCoordinator;

		[Inject]
		public void Construct(DiColorsInfoView infoView, /*DiColorsProfileView profileView,*/ MainFlowCoordinator mainFlowCoordinator)
		{
			_infoView = infoView;
			//_profileView = profileView;
			_mainFlowCoordinator = mainFlowCoordinator;
		}

		protected override void DidActivate(bool firstActivation, ActivationType activationType)
		{
			if (firstActivation)
			{
				title = "DiColors";
				showBackButton = true;
			}
			ProvideInitialViewControllers(_infoView/*, _profileView*/);
		}

		protected override void BackButtonWasPressed(ViewController topViewController)
		{
			base.BackButtonWasPressed(topViewController);
			_mainFlowCoordinator.DismissFlowCoordinator(this);
		}
	}
}