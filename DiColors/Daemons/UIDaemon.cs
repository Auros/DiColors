using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using DiColors.UI;
using System;
using Zenject;

namespace DiColors.Daemons;

internal class UIDaemon : IInitializable, IDisposable
{
    private readonly MenuButton? _menuButton;
    private readonly MainFlowCoordinator _mainFlowCoordinator;
    private readonly DiColorsTestingFlowCoordinator? _testingFlowCoordinator;

    public UIDaemon(MainFlowCoordinator mainFlowCoordinator, [InjectOptional] DiColorsTestingFlowCoordinator testingFlowCoordinator)
    {
        _mainFlowCoordinator = mainFlowCoordinator;
        _testingFlowCoordinator = testingFlowCoordinator;
        if (testingFlowCoordinator != null)
            _menuButton = new MenuButton(nameof(DiColors), MenuButtonClicked);
    }

    public void Initialize()
    {
        if (_menuButton is not null)
            MenuButtons.instance.RegisterButton(_menuButton);
    }

    private void MenuButtonClicked()
    {
        if (_testingFlowCoordinator != null)
            _mainFlowCoordinator.PresentFlowCoordinator(_testingFlowCoordinator);
    }

    public void Dispose()
    {
        if (_menuButton is not null && MenuButtons.IsSingletonAvailable && BSMLParser.IsSingletonAvailable)
            MenuButtons.instance.UnregisterButton(_menuButton);
        MenuButtons.instance.UnregisterButton(_menuButton);
    }
}