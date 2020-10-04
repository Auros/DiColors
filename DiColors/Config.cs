using UnityEngine;
using System.Collections.Generic;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;

namespace DiColors
{
	public class Config
	{
		/*public virtual string SelectedProfile { get; set; }

		public virtual string Name { get; set; } = "Default";*/

		[NonNullable]
		public virtual Game GameSettings { get; set; } = new Game();

		[NonNullable]
		public virtual Menu MenuSettings { get; set; } = new Menu();

		/*public void Copy(Config config)
		{
			var profile = config.Name.Clone() as string;
			CopyFrom(config);
			SelectedProfile = profile;
		}

		public virtual void CopyFrom(Config config)
		{

		}*/

		public class Game
		{
			public virtual bool Enabled { get; set; }

			public virtual string ArrowTexture { get; set; }

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
			public virtual Color DefaultColor { get; set; }

			[NonNullable, UseConverter(typeof(DictionaryConverter<ColorPair>))]
			public virtual Dictionary<string, ColorPair> ColorPairs { get; set; } = new Dictionary<string, ColorPair>
			{
				{ "Freeplay", new ColorPair() },
				{ "Results", new ColorPair() },
				{ "Campaigns", new ColorPair() },
				{ "PlayersPlaceBorder", new ColorPair() },
				{ "PlayersPlaceFeet", new ColorPair() },
			};
		}

		public class ColorPair
		{
			public virtual bool Enabled { get; set; }

			[UseConverter(typeof(HexColorConverter))]
			public virtual Color Color { get; set; }
		}
	}
}