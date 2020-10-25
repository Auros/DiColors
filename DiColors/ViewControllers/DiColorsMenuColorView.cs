using Zenject;
using UnityEngine;
using DiColors.Services;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace DiColors.ViewControllers
{
	// dont look at this class pretend it doesnt exist dont use it as an example, seriously there is some sketch stuff goin on here
	[ViewDefinition("DiColors.Views.menu-color-view.bsml")]
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

		[UIValue("results-fail")]
		protected Color resultsFail
		{
			get => _resultsFailPair.Color;
			set => _resultsFailPair.Color = value;
		}

		[UIValue("results-fail-on")]
		protected bool resultsFailOn
		{
			get => _resultsFailPair.Enabled;
			set => _resultsFailPair.Enabled = value;
		}

		[UIValue("results-fail-show")]
		public bool resultsFailShow { get; set; }
		private Config.ColorPair _resultsFailPair;

		[UIValue("multiplayer-menu")]
		protected Color multiplayerMenu
		{
			get => _multiplayerMenuPair.Color;
			set => _multiplayerMenuPair.Color = value;
		}

		[UIValue("multiplayer-menu-on")]
		protected bool multiplayerMenuOn
		{
			get => _multiplayerMenuPair.Enabled;
			set => _multiplayerMenuPair.Enabled = value;
		}

		[UIValue("multiplayer-menu-show")]
		public bool multiplayerMenuShow { get; set; }
		private Config.ColorPair _multiplayerMenuPair;

		[UIValue("multiplayer-countdown")]
		protected Color multiplayerCountdown
		{
			get => _multiplayerCountdownPair.Color;
			set => _multiplayerCountdownPair.Color = value;
		}

		[UIValue("multiplayer-countdown-on")]
		protected bool multiplayerCountdownOn
		{
			get => _multiplayerCountdownPair.Enabled;
			set => _multiplayerCountdownPair.Enabled = value;
		}

		[UIValue("multiplayer-countdown-show")]
		public bool multiplayerCountdownShow { get; set; }
		private Config.ColorPair _multiplayerCountdownPair;

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


		[UIValue("beat")]
		protected Color beat
		{
			get => _beatPair.Color;
			set => _beatPair.Color = value;
		}

		[UIValue("beat-on")]
		protected bool beatOn
		{
			get => _beatPair.Enabled;
			set => _beatPair.Enabled = value;
		}

		[UIValue("beat-show")]
		public bool beatShow { get; set; }

		private Config.ColorPair _beatPair;

		[UIValue("saber")]
		protected Color saber
		{
			get => _saberPair.Color;
			set => _saberPair.Color = value;
		}

		[UIValue("saber-on")]
		protected bool saberOn
		{
			get => _saberPair.Enabled;
			set => _saberPair.Enabled = value;
		}

		[UIValue("saber-show")]
		public bool saberShow { get; set; }

		private Config.ColorPair _saberPair;

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

			beatShow = _menuConfig.ColorPairs.TryGetValue("Beat", out _beatPair);
			saberShow = _menuConfig.ColorPairs.TryGetValue("Saber", out _saberPair);
			resultsShow = _menuConfig.ColorPairs.TryGetValue("Results", out _resultsPair);
			feetShow = _menuConfig.ColorPairs.TryGetValue("PlayersPlaceFeet", out _feetPair);
			freeplayShow = _menuConfig.ColorPairs.TryGetValue("Freeplay", out _freeplayPair);
			campaignShow = _menuConfig.ColorPairs.TryGetValue("Campaigns", out _campaignPair);
			resultsFailShow = _menuConfig.ColorPairs.TryGetValue("ResultsFail", out _resultsFailPair);
			multiplayerMenuShow = _menuConfig.ColorPairs.TryGetValue("Multiplayer", out _multiplayerMenuPair);
			multiplayerCountdownShow = _menuConfig.ColorPairs.TryGetValue("MultiplayerCountdown", out _multiplayerCountdownPair);

			if (_feetPair == null) { _feetPair = new Config.ColorPair(); }
			if (_beatPair == null) { _beatPair = new Config.ColorPair(); }
			if (_saberPair == null) { _saberPair = new Config.ColorPair(); }
			if (_resultsPair == null) { _resultsPair = new Config.ColorPair(); }
			if (_freeplayPair == null) { _freeplayPair = new Config.ColorPair(); }
			if (_campaignPair == null) { _campaignPair = new Config.ColorPair(); }
			if (_resultsFailPair == null) { _resultsFailPair = new Config.ColorPair(); }
			if (_multiplayerMenuPair == null) { _multiplayerMenuPair = new Config.ColorPair(); }
			if (_multiplayerCountdownPair == null) { _multiplayerCountdownPair = new Config.ColorPair(); }
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
			await SiraUtil.Utilities.AwaitSleep(500);
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