using IPA.Utilities;
using UnityEngine;

namespace DiColors.Coloring;

internal class TopSignColorizer : Colorizer
{
    private Color _color;
    public override string Name => "(Logo) BEAT";

    private readonly SpriteRenderer[] _sprites = new SpriteRenderer[2];
    private readonly ParticleSystem[] _particles = new ParticleSystem[2];
    private readonly TubeBloomPrePassLight[] _lights = new TubeBloomPrePassLight[4];

    private readonly SpriteRenderer _glowLine;
    private readonly FlickeringNeonSign _flickeringNeonSign;

    public const float GlowLineAlpha = 0.251f;
    private static readonly FieldAccessor<FlickeringNeonSign, Color>.Accessor NeonSign_LightOnColor = FieldAccessor<FlickeringNeonSign, Color>.GetAccessor("_lightOnColor");
    private static readonly FieldAccessor<FlickeringNeonSign, Color>.Accessor NeonSign_SpriteOnColor = FieldAccessor<FlickeringNeonSign, Color>.GetAccessor("_spriteOnColor");

    public TopSignColorizer(MenuEnvironmentManager menuEnvironmentManager)
    {
        var memTransform = menuEnvironmentManager.transform;
        var defaultMenuEnv = memTransform.Find("DefaultMenuEnvironment");
        var logo = defaultMenuEnv.Find("Logo");
        var flickering = logo.transform.Find("EFlickering");

        _sprites[0] = logo.Find("BatLogo").GetComponent<SpriteRenderer>();
        _sprites[1] = flickering.Find("LogoE").GetComponent<SpriteRenderer>();
        _glowLine = defaultMenuEnv.Find("GlowLines").GetComponent<SpriteRenderer>();

        _particles[0] = flickering.Find("SparksUp").GetComponent<ParticleSystem>();
        _particles[1] = flickering.Find("SparksDown").GetComponent<ParticleSystem>();

        _lights[0] = logo.Find("BNeon").GetComponent<TubeBloomPrePassLight>();
        _lights[1] = logo.Find("ANeon").GetComponent<TubeBloomPrePassLight>();
        _lights[2] = logo.Find("TNeon").GetComponent<TubeBloomPrePassLight>();
        _lights[3] = flickering.Find("ENeon").GetComponent<TubeBloomPrePassLight>();

        _flickeringNeonSign = flickering.GetComponent<FlickeringNeonSign>();
    }

    public override Color Color
    {
        get => _color;
        set
        {
            _color = value;
            ColorSigns(_color);
        }
    }

    private void ColorSigns(Color color)
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