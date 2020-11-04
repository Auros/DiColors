using System;
using Zenject;
using System.IO;
using UnityEngine;
using IPA.Utilities;
using SiraUtil.Interfaces;

namespace DiColors.Providers
{
    internal class ArrowDecoratorProvider : IModelProvider
    {
        public Type Type => typeof(ArrowDecorator);

        public int Priority { get; set; } = 350;

        private class ArrowDecorator : IPrefabProvider<GameNoteController>
        {
            public bool Chain => true;

            private Config.Game _gameConfig;

            [Inject]
            public void Construct(Config.Game gameConfig)
            {
                _gameConfig = gameConfig;
            }

            public GameNoteController Modify(GameNoteController original)
            {
                var cnv = original.GetComponent<ColorNoteVisuals>();
                if (_gameConfig.Enabled && !string.IsNullOrEmpty(_gameConfig.ArrowTexture) && _gameConfig.ArrowTexture != "Default" && File.Exists(Path.Combine(Constants.TEXTUREDIR, _gameConfig.ArrowTexture)))
                {
                    var spriteRenderer = cnv.GetField<SpriteRenderer, ColorNoteVisuals>("_arrowGlowSpriteRenderer");
                    if (File.Exists(Path.Combine(Constants.TEXTUREDIR, _gameConfig.ArrowTexture)))
                    {
                        var bytes = File.ReadAllBytes(Path.Combine(Constants.TEXTUREDIR, _gameConfig.ArrowTexture));
                        var tex = new Texture2D(2, 2);
                        tex.LoadImage(bytes);
                        var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 460, 0, SpriteMeshType.Tight);
                        spriteRenderer.sprite = sprite;
                    }
                }
                return original;
            }
        }
    }
}
