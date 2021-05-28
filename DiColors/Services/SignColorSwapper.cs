using Zenject;
using UnityEngine;
using IPA.Utilities;
using System.Collections.Generic;

namespace DiColors.Services
{
    public class SignColorSwapper : IInitializable
    {
        private SpriteRenderer _eLogo;
        private SpriteRenderer _batLogo;
		private SpriteRenderer _beatLine;
        private SpriteRenderer _saberLogo;
		private SpriteRenderer _saberLine;
        private TubeBloomPrePassLight _bNeon;
        private TubeBloomPrePassLight _eNeon;
        private TubeBloomPrePassLight _aNeon;
        private TubeBloomPrePassLight _tNeon;
        private TubeBloomPrePassLight _saberNeon;
		private readonly MenuEnvironmentManager _menuEnvironmentManager;
        private readonly FlickeringNeonSign _flickeringNeonSign;
        private readonly Config.Menu _config;

        public SignColorSwapper(Config.Menu config, MenuEnvironmentManager menuEnvironmentManager)
        {
            _config = config;
			_menuEnvironmentManager = menuEnvironmentManager;
			_flickeringNeonSign = menuEnvironmentManager.transform.GetComponentInChildren<FlickeringNeonSign>();
        }

        public void Initialize()
        {
            var parent = _flickeringNeonSign.transform.parent.gameObject;
            var renderers = parent.GetComponentsInChildren<SpriteRenderer>();
            var tubeLights = parent.GetComponentsInChildren<TubeBloomPrePassLight>();
            _eNeon = _flickeringNeonSign.GetField<TubeBloomPrePassLight, FlickeringNeonSign>("_light");
            _eLogo = _flickeringNeonSign.GetField<SpriteRenderer, FlickeringNeonSign>("_flickeringSprite");
			var rootRenderers = new List<SpriteRenderer>();
			var defaultEnvironment = _menuEnvironmentManager.transform.GetChild(0);
			for (int i = defaultEnvironment.childCount - 1; i >= 0; i--)
			{
				var child = defaultEnvironment.GetChild(i);
				var renderer = child.GetComponent<SpriteRenderer>();
				if (renderer != null)
				{
					rootRenderers.Add(renderer);
				}
				if (rootRenderers.Count >= 2)
				{
					break;
				}	
			}
			if (rootRenderers.Count >= 2)
			{
				_beatLine = rootRenderers[1];
				_saberLine = rootRenderers[0];
			}

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
                    case "BNeon":
                        _bNeon = light;
						break;
					case "ANeon":
                        _aNeon = light;
						break;
					case "TNeon":
                        _tNeon = light;
                        break;
                    case "SaberNeon":
                        _saberNeon = light;
                        break;
                }
            }
            if (_config.ColorPairs.TryGetValue("Beat", out Config.ColorPair beatPair) && beatPair.Enabled)
            {
                _batLogo.color = beatPair.Color;
                _eLogo.color = beatPair.Color;
                _eNeon.color = beatPair.Color;
                _bNeon.color = beatPair.Color;
                _aNeon.color = beatPair.Color;
                _tNeon.color = beatPair.Color;
                _flickeringNeonSign.SetField("_lightOnColor", beatPair.Color);
                _flickeringNeonSign.SetField("_spriteOnColor", beatPair.Color);
				_beatLine.color = new Color(beatPair.Color.r, beatPair.Color.g, beatPair.Color.b, _beatLine.color.a);
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
				_saberLine.color = new Color(saberPair.Color.r, saberPair.Color.g, saberPair.Color.b, _saberLine.color.a);
            }
        }
    }
}