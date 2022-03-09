using IPA.Utilities;
using UnityEngine;

namespace DiColors.Coloring;

internal class LevelFailedColorizer : Colorizer
{
    public override string Name => "Level Failed";

    private static readonly FieldAccessor<SoloFreePlayFlowCoordinator, MenuLightsPresetSO>.Accessor SoloFreePlay_FailedPreset = FieldAccessor<SoloFreePlayFlowCoordinator, MenuLightsPresetSO>.GetAccessor("_resultsFailedLightsPreset");

    private readonly MenuLightsPresetSO _failedPreset;

    public LevelFailedColorizer(SoloFreePlayFlowCoordinator soloFreePlayFlowCoordinator)
    {
        _failedPreset = SoloFreePlay_FailedPreset(ref soloFreePlayFlowCoordinator);
        DuplicateInternalSimpleColors(_failedPreset);
    }

    public override Color Color
    {
        get => _config.FailedLevelColor;
        set
        {
            _config.FailedLevelColor = value;
            SetColorForPreset(_failedPreset, _config.FailedLevelColor);
        }
    }
}