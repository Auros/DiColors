using Zenject;
using UnityEngine;
using IPA.Utilities;

namespace DiColors.Services
{
    public class SignColorSwapper : IInitializable
    {
        private SpriteRenderer _eLogo;
        private SpriteRenderer _batLogo;
        private SpriteRenderer _saberLogo;
        private TubeBloomPrePassLight _eNeon;
        private TubeBloomPrePassLight _batNeon;
        private TubeBloomPrePassLight _saberNeon;
        private readonly FlickeringNeonSign _flickeringNeonSign;
        private readonly Config.Menu _config;

        public SignColorSwapper(Config.Menu config, FlickeringNeonSign flickeringNeonSign)
        {
            _config = config;
            _flickeringNeonSign = flickeringNeonSign;
        }

        public void Initialize()
        {
            var parent = _flickeringNeonSign.transform.parent.gameObject;
            var renderers = parent.GetComponentsInChildren<SpriteRenderer>();
            var tubeLights = parent.GetComponentsInChildren<TubeBloomPrePassLight>();
            _eNeon = _flickeringNeonSign.GetField<TubeBloomPrePassLight, FlickeringNeonSign>("_light");
            _eLogo = _flickeringNeonSign.GetField<SpriteRenderer, FlickeringNeonSign>("_flickeringSprite");

            foreach (var renderer in renderers)
            {
                switch (renderer.gameObject.name)
                {
                    case "BatLogo":
                        _batLogo = renderer;
                        break;
                    case "SaberLogo":
                        _saberLogo = renderer;
                        break;
                }
            }
            foreach (var light in tubeLights)
            {
                switch (light.gameObject.name)
                {
                    case "BATNeon":
                        _batNeon = light;
                        break;
                    case "SaberNeon":
                        _saberNeon = light;
                        break;
                }
            }
            if (_config.ColorPairs.TryGetValue("Beat", out Config.ColorPair beatPair) && beatPair.Enabled)
            {
                _eLogo.color = beatPair.Color;
                _eNeon.color = beatPair.Color;
                _batLogo.color = beatPair.Color;
                _batNeon.color = beatPair.Color;
                _flickeringNeonSign.SetField("_lightOnColor", beatPair.Color);
                _flickeringNeonSign.SetField("_spriteOnColor", beatPair.Color);
                var pss = _flickeringNeonSign.gameObject.GetComponentsInChildren<ParticleSystem>();
                foreach (var ps in pss)
                {
                    var main = ps.main;
                    main.startColor = beatPair.Color;
                }
            }
            if (_config.ColorPairs.TryGetValue("Saber", out Config.ColorPair saberPair) && saberPair.Enabled)
            {
                _saberLogo.color = saberPair.Color;
                _saberNeon.color = saberPair.Color;
            }
        }
    }
}