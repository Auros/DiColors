using Zenject;
using UnityEngine;
using System.Threading;
using DiColors.Services;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace DiColors.ViewControllers
{
	// dont look at this class pretend it doesnt exist dont use it as an example, seriously there is some sketch stuff goin on here
	[HotReload(RelativePathToLayout = @"..\Views\menu-color-view.bsml")]
	public class DiColorsMenuColorView : BSMLAutomaticViewController
	{
		private Config.Menu _menuConfig;
		private Config.Menu _stashedConfig;
		private FadeInOutController _fader;
		private MenuColorSwapper _colorSwapper;
		private MenuTransitionsHelper _transitioner;

		[UIValue("default")]
		protected Color Default
		{
			get => _menuConfig.DefaultColor;
			set => _menuConfig.DefaultColor = value;
		}

		[UIValue("default-on")]
		protected bool defaultOn
		{
			get => _menuConfig.Enabled;
			set => _menuConfig.Enabled = value;
		}

		[UIValue("campaign")]
		protected Color campaign
		{
			get => _campaignPair.Color;
			set => _campaignPair.Color = value;
		}

		[UIValue("campaign-on")]
		protected bool campaignOn
		{
			get => _campaignPair.Enabled;
			set => _campaignPair.Enabled = value;
		}

		[UIValue("campaign-show")]
		public bool campaignShow { get; set; }

		private Config.ColorPair _campaignPair;

		[UIValue("freeplay")]
		protected Color freeplay
		{
			get => _freeplayPair.Color;
			set => _freeplayPair.Color = value;
		}

		[UIValue("freeplay-on")]
		protected bool freeplayOn
		{
			get => _freeplayPair.Enabled;
			set => _freeplayPair.Enabled = value;
		}

		[UIValue("freeplay-show")]
		public bool freeplayShow { get; set; }

		private Config.ColorPair _freeplayPair;

		[UIValue("results")]
		protected Color results
		{
			get => _resultsPair.Color;
			set => _resultsPair.Color = value;
		}

		[UIValue("results-on")]
		protected bool resultsOn
		{
			get => _resultsPair.Enabled;
			set => _resultsPair.Enabled = value;
		}

		[UIValue("results-show")]
		public bool resultsShow { get; set; }

		private Config.ColorPair _resultsPair;

		[UIValue("feet")]
		protected Color feet
		{
			get => _feetPair.Color;
			set => _feetPair.Color = value;
		}

		[UIValue("feet-on")]
		protected bool feetOn
		{
			get => _feetPair.Enabled;
			set => _feetPair.Enabled = value;
		}

		[UIValue("feet-show")]
		public bool feetShow { get; set; }

		private Config.ColorPair _feetPair;

		[UIValue("border")]
		protected Color border
		{
			get => _borderPair.Color;
			set => _borderPair.Color = value;
		}

		[UIValue("border-on")]
		protected bool borderOn
		{
			get => _borderPair.Enabled;
			set => _borderPair.Enabled = value;
		}

		[UIValue("border-show")]
		public bool borderShow { get; set; }

		private Config.ColorPair _borderPair;

		[UIParams]
		protected BSMLParserParams parserParams;

		[Inject]
		public void Construct(Config.Menu menuConfig, MenuColorSwapper menuColorSwapper, FadeInOutController fader, MenuTransitionsHelper transitioner)
		{
			_fader = fader;
			_menuConfig = menuConfig;
			_transitioner = transitioner;
			_colorSwapper = menuColorSwapper;
			_stashedConfig = _menuConfig.Copy();

			_menuConfig.ColorPairs.TryGetValue("Freeplay", out _freeplayPair);
			_menuConfig.ColorPairs.TryGetValue("Results", out _resultsPair);
			_menuConfig.ColorPairs.TryGetValue("Campaigns", out _campaignPair);
			_menuConfig.ColorPairs.TryGetValue("PlayersPlaceBorder", out _borderPair);
			_menuConfig.ColorPairs.TryGetValue("PlayersPlaceFeet", out _feetPair);

			freeplayShow = !(_freeplayPair is null);
			resultsShow = !(_resultsPair is null);
			campaignShow = !(_campaignPair is null);
			borderShow = !(_borderPair is null);
			feetShow = !(_feetPair is null);
		}

		[UIAction("on-reset")]
		protected void Reset()
		{
			Apply(_menuConfig, _stashedConfig);
			_colorSwapper.UpdateColors(_stashedConfig);
			parserParams.EmitEvent("get");
		}

		[UIAction("on-apply")]
		protected async void Apply()
		{
			parserParams.EmitEvent("apply");
			_fader.FadeOut();
			await Task.Run(() => Thread.Sleep(500));
			_transitioner.RestartGame();
		}

		private void Apply(Config.Menu toApplyTo, Config.Menu donor)
		{
			toApplyTo.Enabled = donor.Enabled;
			toApplyTo.ColorPairs = donor.ColorPairs;
			toApplyTo.DefaultColor = donor.DefaultColor;
		}
	}
}