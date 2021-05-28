using HMUI;
using Zenject;
using BeatSaberMarkupLanguage;
using DiColors.ViewControllers;

namespace DiColors
{
	public class DiColorsFlowCoordinator : FlowCoordinator
    {
        private DiColorsInfoView _infoView;
        private DiColorsMenuColorView _menuColorView;
        private DiColorsGameColorView _gameColorView;
        private MainFlowCoordinator _mainFlowCoordinator;

        [Inject]
        public void Construct(DiColorsInfoView infoView, DiColorsMenuColorView menuColorView, DiColorsGameColorView gameColorView, MainFlowCoordinator mainFlowCoordinator)
        {
            _infoView = infoView;
            //_profileView = profileView;
            _menuColorView = menuColorView;
            _gameColorView = gameColorView;
            _mainFlowCoordinator = mainFlowCoordinator;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                SetTitle("DiColors");
                showBackButton = true;

				ProvideInitialViewControllers(_infoView, _menuColorView /*_gameColorView, _profileView*/);
            }
			SetLeftScreenViewController(_menuColorView, ViewController.AnimationType.In);
			//SetRightScreenViewController(_gameColorView, ViewController.AnimationType.In);
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            base.BackButtonWasPressed(topViewController);
            _mainFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}