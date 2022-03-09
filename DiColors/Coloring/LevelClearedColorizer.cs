using IPA.Utilities;
using UnityEngine;

namespace DiColors.Coloring;

internal class LevelClearedColorizer : Colorizer
{
    public override string Name => "Level Cleared";

    private static readonly FieldAccessor<SoloFreePlayFlowCoordinator, MenuLightsPresetSO>.Accessor SoloFreePlay_ClearedPreset = FieldAccessor<SoloFreePlayFlowCoordinator, MenuLightsPresetSO>.GetAccessor("_resultsClearedLightsPreset");

    private readonly MenuLightsPresetSO _clearedPreset;

    public LevelClearedColorizer(SoloFreePlayFlowCoordinator soloFreePlayFlowCoordinator)
    {
        _clearedPreset = SoloFreePlay_ClearedPreset(ref soloFreePlayFlowCoordinator);
        DuplicateInternalSimpleColors(_clearedPreset);
    }

    public override Color Color
    {
        get => _config.ClearedLevelColor;
        set
        {
            _config.ClearedLevelColor = value;
            SetColorForPreset(_clearedPreset, _config.ClearedLevelColor);
        }
    }
}