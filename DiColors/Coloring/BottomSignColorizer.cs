using DiColors.Services;
using UnityEngine;

namespace DiColors.Coloring;

internal class BottomSignColorizer : Colorizer
{
    private Color _color;
    private readonly SpriteRenderer _glowLine;
    private readonly SpriteRenderer _saberLogo;
    private readonly TubeBloomPrePassLight _glowLight;

    public override string Name => "(Logo) SABER";

    public override Color Color
    {
        get => _color;
        set
        {
            _color = value;
            ColorSign(_color);
        }
    }

    public BottomSignColorizer(MenuTransformAccessor menuTransformAccessor)
    {
        _saberLogo = menuTransformAccessor.Logo.Find("SaberLogo").GetComponent<SpriteRenderer>();
        _glowLight = menuTransformAccessor.Logo.Find("SaberNeon").GetComponent<TubeBloomPrePassLight>();
        _glowLine = menuTransformAccessor.DefaultMenuEnvironment.Find("GlowLines (1)").GetComponent<SpriteRenderer>();
    }

    private void ColorSign(Color color)
    {
        _glowLine.color = color.ColorWithAlpha(TopSignColorizer.GlowLineAlpha);
        _saberLogo.color = color;
        _glowLight.color = color;
    }
}