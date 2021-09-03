using System.Collections.Generic;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using SiraUtil.Converters;
using UnityEngine;
using Version = Hive.Versioning.Version;

namespace DiColors
{
	public class Config
    {
        /*public virtual string SelectedProfile { get; set; }

        public virtual string Name { get; set; } = "Default";*/

        [NonNullable, UseConverter(typeof(HiveVersionConverter))]
        public Version Version { get; set; } = new Version("1.0.0");

		[NonNullable]
		public Game GameSettings { get; set; } = new Game();

		[NonNullable]
		public Menu MenuSettings { get; set; } = new Menu();

		public virtual void Changed() { /* Force BSIPA to Save */ }

		public class Game
		{
			[Ignore]
			public Config Config { get; internal set; }

			public void SaveIt()
			{
				Config.Changed();
			}

			public bool Enabled { get; set; }

            public string ArrowTexture { get; set; } = "Default";

            public bool UseLeftColor { get; set; }

            public bool UseRightColor { get; set; }

            [UseConverter(typeof(HexColorConverter))]
            public Color LeftArrowColor { get; set; }

            [UseConverter(typeof(HexColorConverter))]
            public Color RightArrowColor { get; set; }
        }

        public class Menu
		{
			[Ignore]
			public Config Config { get; internal set; }

			public void SaveIt()
			{
				Config.Changed();
			}

            public bool Enabled { get; set; }

            [UseConverter(typeof(HexColorConverter))]
            public Color DefaultColor { get; set; } = Color.cyan;

            [NonNullable, UseConverter(typeof(DictionaryConverter<ColorPair>))]
            public Dictionary<string, ColorPair> ColorPairs { get; set; } = new Dictionary<string, ColorPair>
            {
                { "Freeplay", new ColorPair() },
                { "Results", new ColorPair() },
                { "ResultsFail", new ColorPair() },
                { "Campaigns", new ColorPair() },
                { "PlayersPlaceFeet", new ColorPair() },
                { "Multiplayer", new ColorPair() },
                { "MultiplayerCountdown", new ColorPair() },
                { "Beat", new ColorPair() },
                { "Saber", new ColorPair() }
            };
        }

        public class ColorPair
        {
            public bool Enabled { get; set; }

            [UseConverter(typeof(HexColorConverter))]
            public Color Color { get; set; } = Color.white;
        }
    }
}