using SemVer;
using UnityEngine;
using SiraUtil.Converters;
using System.Collections.Generic;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;

namespace DiColors
{
	public class Config
	{
		/*public virtual string SelectedProfile { get; set; }

		public virtual string Name { get; set; } = "Default";*/

		[NonNullable, UseConverter(typeof(VersionConverter))]
		public virtual Version Version { get; set; } = new Version("1.0.0");

		[NonNullable]
		public virtual Game GameSettings { get; set; } = new Game();

		[NonNullable]
		public virtual Menu MenuSettings { get; set; } = new Menu();

		/*public void Copy(Config config)
		{
			var profile = config.Name.Clone() as string;
			CopyFrom(config);
			SelectedProfile = profile;
		}*/

		public virtual void CopyFrom(Config config)
		{

		}

		public class Game
		{
			public virtual bool Enabled { get; set; }

			public virtual string ArrowTexture { get; set; } = "Default";

			public virtual bool UseLeftColor { get; set; }

			public virtual bool UseRightColor { get; set; }

			[UseConverter(typeof(HexColorConverter))]
			public virtual Color LeftArrowColor { get; set; }

			[UseConverter(typeof(HexColorConverter))]
			public virtual Color RightArrowColor { get; set; }
		}

		public class Menu
		{
			public virtual bool Enabled { get; set; }

			[UseConverter(typeof(HexColorConverter))]
			public virtual Color DefaultColor { get; set; } = Color.cyan;

			[NonNullable, UseConverter(typeof(DictionaryConverter<ColorPair>))]
			public virtual Dictionary<string, ColorPair> ColorPairs { get; set; } = new Dictionary<string, ColorPair>
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
			public virtual bool Enabled { get; set; }

			[UseConverter(typeof(HexColorConverter))]
			public virtual Color Color { get; set; } = Color.white;
		}
	}
}