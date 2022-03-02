using DiColors.Services;
using IPA.Utilities;
using UnityEngine;

namespace DiColors.Coloring;

internal class TopSignColorizer : Colorizer
{
    private readonly SpriteRenderer[] _sprites = new SpriteRenderer[2];
    private readonly ParticleSystem[] _particles = new ParticleSystem[2];
    private readonly TubeBloomPrePassLight[] _lights = new TubeBloomPrePassLight[4];

    private readonly SpriteRenderer _glowLine;
    private readonly FlickeringNeonSign _flickeringNeonSign;

    public const float GlowLineAlpha = 0.251f;
    private static readonly FieldAccessor<FlickeringNeonSign, Color>.Accessor NeonSign_LightOnColor = FieldAccessor<FlickeringNeonSign, Color>.GetAccessor("_lightOnColor");
    private static readonly FieldAccessor<FlickeringNeonSign, Color>.Accessor NeonSign_SpriteOnColor = FieldAccessor<FlickeringNeonSign, Color>.GetAccessor("_spriteOnColor");

    public override string Name => "(Logo) BEAT";
    public override Color Color
    {
        get => _config.LogoBeatColor;
        set
        {
            _config.LogoBeatColor = value;
            ColorSign(_config.LogoBeatColor);
        }
    }

    public TopSignColorizer(MenuTransformAccessor menuTransformAccessor)
    {
        var flickering = menuTransformAccessor.Logo.transform.Find("EFlickering");

        _flickeringNeonSign = flickering.GetComponent<FlickeringNeonSign>();
        _sprites[1] = flickering.Find("LogoE").GetComponent<SpriteRenderer>();
        _particles[0] = flickering.Find("SparksUp").GetComponent<ParticleSystem>();
        _lights[3] = flickering.Find("ENeon").GetComponent<TubeBloomPrePassLight>();
        _particles[1] = flickering.Find("SparksDown").GetComponent<ParticleSystem>();
        _sprites[0] = menuTransformAccessor.Logo.Find("BatLogo").GetComponent<SpriteRenderer>();
        _lights[0] = menuTransformAccessor.Logo.Find("BNeon").GetComponent<TubeBloomPrePassLight>();
        _lights[1] = menuTransformAccessor.Logo.Find("ANeon").GetComponent<TubeBloomPrePassLight>();
        _lights[2] = menuTransformAccessor.Logo.Find("TNeon").GetComponent<TubeBloomPrePassLight>();
        _glowLine = menuTransformAccessor.DefaultMenuEnvironment.Find("GlowLines").GetComponent<SpriteRenderer>();
    }

    private void ColorSign(Color color)
    {
        foreach (var sprite in _sprites)
            sprite.color = color;

        foreach (var particle in _particles)
        {
            var main = particle.main;
            main.startColor = color;
        }

        foreach (var light in _lights)
            light.color = color;

        _glowLine.color = color.ColorWithAlpha(GlowLineAlpha);

        var sign = _flickeringNeonSign;
        NeonSign_LightOnColor(ref sign) = color;
        NeonSign_SpriteOnColor(ref sign) = color;
    }
}