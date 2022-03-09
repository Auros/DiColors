using IPA.Utilities;
using Tweening;
using UnityEngine;
using static MenuLightsPresetSO;

namespace DiColors.Coloring;

internal class DefaultMenuColorizer : Colorizer
{
    public override string Name => "Default Menu";

    private Tween? _tween;
    private readonly MenuLightsManager _menuLightsManager;

    public const int MenuColorLightId = 1;

    private static readonly FieldAccessor<MenuLightsPresetSO, LightIdColorPair[]>.Accessor LightPreset_ColorPairs = FieldAccessor<MenuLightsPresetSO, LightIdColorPair[]>.GetAccessor("_lightIdColorPairs");
    private static readonly FieldAccessor<MenuLightsManager, MenuLightsPresetSO>.Accessor MenuLightsManager_DefaultPreset = FieldAccessor<MenuLightsManager, MenuLightsPresetSO>.GetAccessor("_defaultPreset");

    public DefaultMenuColorizer(MenuLightsManager menuLightsManager)
    {
        _menuLightsManager = menuLightsManager;
    }

    public override Color Color
    {
        get => _config.DefaultMenuColor;
        set
        {
            _config.DefaultMenuColor = value;
            SetColor(_config.DefaultMenuColor);
        }
    }

    private void SetColor(Color color)
    {
        _menuLightsManager.SetColor(MenuColorLightId, color);

        Color? oldColor = null;

        var mlm = _menuLightsManager;
        var preset = MenuLightsManager_DefaultPreset(ref mlm);
        var colorPairs = LightPreset_ColorPairs(ref preset);
        foreach (var pair in colorPairs)
        {
            if (pair.lightId != MenuColorLightId)
                continue;

            if (pair.baseColor is SimpleColorSO simpleColor)
            {
                oldColor = pair.baseColor;
                simpleColor.SetColor(color);
                break;
            }
        }

        if (!oldColor.HasValue)
            return;

        _tween?.Kill();
        _tween = new ColorTween(oldColor.Value, color, u => _menuLightsManager.SetColor(MenuColorLightId, u), 0.5f, EaseType.Linear);

        _tween.onCompleted += delegate () { _tween = null; };
        _tweeningManager.AddTween(_tween, _menuLightsManager);
    }
}