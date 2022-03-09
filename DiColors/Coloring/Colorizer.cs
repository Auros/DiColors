using IPA.Utilities;
using SiraUtil.Interfaces;
using Tweening;
using UnityEngine;
using Zenject;
using static MenuLightsPresetSO;

namespace DiColors.Coloring;

internal abstract class Colorizer : IColorable
{
    public abstract string Name { get; }
    public abstract Color Color { get; set; }

    [Inject]
    protected readonly Config _config = null!;

    [Inject]
    protected readonly TimeTweeningManager _tweeningManager = null!;

    public const int MenuColorLightId = 1;

    public static readonly FieldAccessor<MenuLightsPresetSO, LightIdColorPair[]>.Accessor LightPreset_ColorPairs = FieldAccessor<MenuLightsPresetSO, LightIdColorPair[]>.GetAccessor("_lightIdColorPairs");

    public static void SetColorForPreset(MenuLightsPresetSO preset, Color color)
    {
        var colorPairs = LightPreset_ColorPairs(ref preset);
        foreach (var pair in colorPairs)
        {
            if (pair.lightId != MenuColorLightId)
                continue;

            if (pair.baseColor is SimpleColorSO simpleColor)
            {
                simpleColor.SetColor(color);
                break;
            }
        }
    }

    public static SimpleColorSO CreateColorSO(Color color)
    {
        var colorSO = ScriptableObject.CreateInstance<SimpleColorSO>();
        colorSO.SetColor(color);
        return colorSO;
    }

    public static void DuplicateInternalSimpleColors(MenuLightsPresetSO preset)
    {
        var colorPairs = LightPreset_ColorPairs(ref preset);
        for (int i = 0; i < colorPairs.Length; i++)
            colorPairs[i].baseColor = CreateColorSO(colorPairs[i].baseColor);
    }
}