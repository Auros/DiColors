using IPA.Utilities;
using UnityEngine;

namespace DiColors.Coloring;

internal class CountdownColorizer : Colorizer
{
    public override string Name => "Multiplayer Countdown";

    private static readonly FieldAccessor<CenterStageScreenController, MenuLightsPresetSO>.Accessor CenterStageScreen_CountdownPreset = FieldAccessor<CenterStageScreenController, MenuLightsPresetSO>.GetAccessor("_countdownMenuLightsPreset");

    private readonly MenuLightsPresetSO _countdownPreset;

    public CountdownColorizer(CenterStageScreenController centerStageScreenController)
    {
        _countdownPreset = CenterStageScreen_CountdownPreset(ref centerStageScreenController);
        DuplicateInternalSimpleColors(_countdownPreset);
    }

    public override Color Color
    {
        get => _config.CountdownColor;
        set
        {
            _config.CountdownColor = value;
            SetColorForPreset(_countdownPreset, _config.CountdownColor);
        }
    }
}