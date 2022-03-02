using BeatSaberMarkupLanguage;
using HMUI;
using Zenject;

namespace DiColors.UI;

internal class DiColorsTestingFlowCoordinator : FlowCoordinator
{
    [Inject]
    protected readonly DiColorsView _diColorsView = null!;

    [Inject]
    protected readonly MainFlowCoordinator _mainFlowCoordinator = null!;

    protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
    {
        if (firstActivation)
        {
            showBackButton = true;
            SetTitle(nameof(DiColors));
            ProvideInitialViewControllers(_diColorsView);
        }
    }

    protected override void BackButtonWasPressed(ViewController topViewController)
    {
        _mainFlowCoordinator.DismissFlowCoordinator(this);
    }
}