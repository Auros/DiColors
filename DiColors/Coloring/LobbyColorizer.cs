using IPA.Utilities;
using UnityEngine;

namespace DiColors.Coloring;

internal class LobbyColorizer : Colorizer
{
    public override string Name => "Multiplayer Lobby";

    private static readonly FieldAccessor<CenterStageScreenController, MenuLightsPresetSO>.Accessor CenterStageScreen_LobbyPreset = FieldAccessor<CenterStageScreenController, MenuLightsPresetSO>.GetAccessor("_lobbyLightsPreset");

    private readonly MenuLightsPresetSO _lobbyPreset;

    public LobbyColorizer(CenterStageScreenController centerStageScreenController)
    {
        _lobbyPreset = CenterStageScreen_LobbyPreset(ref centerStageScreenController);
        DuplicateInternalSimpleColors(_lobbyPreset);
    }

    public override Color Color
    {
        get => _config.LobbyColor;
        set
        {
            _config.LobbyColor = value;
            SetColorForPreset(_lobbyPreset, _config.LobbyColor);
        }
    }
}