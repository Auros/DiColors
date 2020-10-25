using Zenject;
using UnityEngine;
using IPA.Utilities;
using SiraUtil.Interfaces;
using System.Collections.Generic;

namespace DiColors.Services
{
	public class MenuColorSwapper : IInitializable, IColorable
	{
		private readonly Config.Menu _menuConfig;
		private readonly MenuLightsManager _menuLightsManager;
		private readonly MainFlowCoordinator _mainFlowCoordinator;
		private readonly CampaignFlowCoordinator _campaignFlowCoordinator;
		private readonly CenterStageScreenController _centerStageScreenController;
		private readonly SoloFreePlayFlowCoordinator _soloFreePlayFlowCoordinator;
		private readonly PartyFreePlayFlowCoordinator _partyFreePlayFlowCoordinator;
		private readonly Dictionary<Color, SimpleColorSO> _colorDict = new Dictionary<Color, SimpleColorSO>();
		private readonly Dictionary<Color, MenuLightsPresetSO> _lightsDict = new Dictionary<Color, MenuLightsPresetSO>();

		private SpriteRenderer feetSprite;
		private MenuLightsPresetSO defaultMenuLights;

		public MenuColorSwapper(Config.Menu menuConfig, MenuLightsManager menuLightsManager, MainFlowCoordinator mainFlowCoordinator, CampaignFlowCoordinator campaignFlowCoordinator,
								SoloFreePlayFlowCoordinator soloFreePlayFlowCoordinator, PartyFreePlayFlowCoordinator partyFreePlayFlowCoordinator, CenterStageScreenController centerStageScreenController)
		{
			_menuConfig = menuConfig;
			_menuLightsManager = menuLightsManager;
			_mainFlowCoordinator = mainFlowCoordinator;
			_campaignFlowCoordinator = campaignFlowCoordinator;
			_centerStageScreenController = centerStageScreenController;
			_soloFreePlayFlowCoordinator = soloFreePlayFlowCoordinator;
			_partyFreePlayFlowCoordinator = partyFreePlayFlowCoordinator;
		}

		public Color Color => _menuConfig.Enabled ? _menuConfig.DefaultColor : defaultMenuLights.lightIdColorPairs[0].baseColor;

		public void Initialize()
		{
			defaultMenuLights = _menuLightsManager.GetField<MenuLightsPresetSO, MenuLightsManager>("_defaultPreset");
			var playersPlace = GameObject.Find("PlayersPlace");
			feetSprite = playersPlace.GetComponentInChildren<SpriteRenderer>();
			UpdateColors(_menuConfig);
		}

		public void UpdateColors(Config.Menu menuConfig)
		{
			var defaultColor = CachedColor(Color);
			defaultMenuLights = CreateMenuLights(defaultColor);
			if (menuConfig.ColorPairs.TryGetValue("PlayersPlaceFeet", out Config.ColorPair playersFeetColorPair))
			{
				feetSprite.color = playersFeetColorPair.Enabled ? playersFeetColorPair.Color : defaultColor.color;
			}
			if (menuConfig.ColorPairs.TryGetValue("Freeplay", out Config.ColorPair freeplay))
			{
				if (freeplay.Enabled)
				{
					var lights = CreateMenuLights(freeplay.Color);
					_soloFreePlayFlowCoordinator.SetField("_defaultLightsPreset", lights);
					_partyFreePlayFlowCoordinator.SetField("_defaultLightsPreset", lights);
				}
				else
				{
					_soloFreePlayFlowCoordinator.SetField("_defaultLightsPreset", defaultMenuLights);
					_partyFreePlayFlowCoordinator.SetField("_defaultLightsPreset", defaultMenuLights);
				} 
			}
			if (menuConfig.ColorPairs.TryGetValue("Results", out Config.ColorPair results))
			{
				if (results.Enabled)
				{
					var lights = CreateMenuLights(results.Color);
					_soloFreePlayFlowCoordinator.SetField("_resultsClearedLightsPreset", lights);
					_partyFreePlayFlowCoordinator.SetField("_resultsClearedLightsPreset", lights);
				}
				else
				{
					_soloFreePlayFlowCoordinator.SetField("_resultsClearedLightsPreset", defaultMenuLights);
					_partyFreePlayFlowCoordinator.SetField("_resultsClearedLightsPreset", defaultMenuLights);
				}
			}
			if (menuConfig.ColorPairs.TryGetValue("Campaigns", out Config.ColorPair campaigns))
			{
				if (campaigns.Enabled)
				{
					var lights = CreateMenuLights(campaigns.Color);
					_campaignFlowCoordinator.SetField("_defaultLightsPreset", lights);
				}
				else
				{
					_campaignFlowCoordinator.SetField("_defaultLightsPreset", defaultMenuLights);
				} 
			}
			_centerStageScreenController.SetField("_defaultMenuLightsPreset", defaultMenuLights);
			if (menuConfig.ColorPairs.TryGetValue("Multiplayer", out Config.ColorPair multiplayer))
			{
				if (multiplayer.Enabled)
				{
					var lights = CreateMenuLights(multiplayer.Color);
					_centerStageScreenController.SetField("_lobbyLightsPreset", lights);
				}
			}
			if (menuConfig.ColorPairs.TryGetValue("MultiplayerCountdown", out Config.ColorPair multiplayerCountdown))
			{
				if (multiplayerCountdown.Enabled)
				{
					var lights = CreateMenuLights(multiplayerCountdown.Color);
					_centerStageScreenController.SetField("_countdownMenuLightsPreset", lights);
				}
			}
			_mainFlowCoordinator.SetField("_defaultLightsPreset", defaultMenuLights);
		}

		public MenuLightsPresetSO CreateMenuLights(Color color)
		{
			var cachedColor = CachedColor(color);
			if (_lightsDict.TryGetValue(color, out var menuLightsPresetSO))
			{
				return menuLightsPresetSO;
			}
			menuLightsPresetSO = Object.Instantiate(defaultMenuLights);
			var colorPairs = menuLightsPresetSO.GetField<MenuLightsPresetSO.LightIdColorPair[], MenuLightsPresetSO>("_lightIdColorPairs");
			for (int i = 0; i < colorPairs.Length; i++)
			{
				colorPairs[i] = new MenuLightsPresetSO.LightIdColorPair
				{
					lightId = colorPairs[i].lightId,
					baseColor = cachedColor,
					intensity = colorPairs[i].intensity
				};
			}
			_lightsDict.Add(color, menuLightsPresetSO);
			return menuLightsPresetSO;
		}

		public ColorSO CachedColor(Color color)
		{
			if (_colorDict.TryGetValue(color, out var colorSO))
			{
				return colorSO;
			}
			colorSO = ScriptableObject.CreateInstance<SimpleColorSO>();
			colorSO.SetColor(color);
			_colorDict.Add(color, colorSO);
			return colorSO;
		}

		public void SetColor(Color color)
		{
			defaultMenuLights = _menuLightsManager.GetField<MenuLightsPresetSO, MenuLightsManager>("_defaultPreset");
			_menuLightsManager?.SetColorPreset(CreateMenuLights(color), true);
			_menuLightsManager?.RefreshColors();

		}
	}
}