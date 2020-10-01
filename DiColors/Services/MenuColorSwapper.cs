using Zenject;
using UnityEngine;
using IPA.Utilities;

namespace DiColors.Services
{
	public class MenuColorSwapper : IInitializable
	{
		private readonly MenuLightsManager _menuLightsManager;
		private readonly MainFlowCoordinator _mainFlowCoordinator;
		private readonly CampaignFlowCoordinator _campaignFlowCoordinator;
		private readonly SoloFreePlayFlowCoordinator _soloFreePlayFlowCoordinator;
		private readonly PartyFreePlayFlowCoordinator _partyFreePlayFlowCoordinator;

		private SpriteRenderer feetSprite;
		private RectangleFakeGlow rectangleFakeGlow;

		public MenuColorSwapper(MenuLightsManager menuLightsManager, MainFlowCoordinator mainFlowCoordinator, CampaignFlowCoordinator campaignFlowCoordinator,
								SoloFreePlayFlowCoordinator soloFreePlayFlowCoordinator, PartyFreePlayFlowCoordinator partyFreePlayFlowCoordinator)
		{
			_menuLightsManager = menuLightsManager;
			_mainFlowCoordinator = mainFlowCoordinator;
			_campaignFlowCoordinator = campaignFlowCoordinator;
			_soloFreePlayFlowCoordinator = soloFreePlayFlowCoordinator;
			_partyFreePlayFlowCoordinator = partyFreePlayFlowCoordinator;
		}

		public void Initialize()
		{
			SimpleColorSO colorSO = ScriptableObject.CreateInstance<SimpleColorSO>();
			colorSO.SetColor(Color.red.ColorWithAlpha(10f));

			var playersPlace = GameObject.Find("MenuPlayersPlace");
			feetSprite = playersPlace.GetComponentInChildren<SpriteRenderer>();
			rectangleFakeGlow = playersPlace.GetComponentInChildren<RectangleFakeGlow>();

			feetSprite.color = colorSO.color;
			rectangleFakeGlow.color = colorSO.color;

			MenuLightsPresetSO menuLightsPresetSO = _menuLightsManager.GetField<MenuLightsPresetSO, MenuLightsManager>("_defaultPreset");
			menuLightsPresetSO.SetField<MenuLightsPresetSO, ColorSO>("_playersPlaceNeonsColor", colorSO);
			var colorPairs = menuLightsPresetSO.GetField<MenuLightsPresetSO.LightIdColorPair[], MenuLightsPresetSO>("_lightIdColorPairs");
			for (int i = 0; i < colorPairs.Length; i++)
			{
				colorPairs[i].SetField<MenuLightsPresetSO.LightIdColorPair, ColorSO>("baseColor", colorSO);
			}
			_mainFlowCoordinator.SetField("_defaultLightsPreset", menuLightsPresetSO);
			_campaignFlowCoordinator.SetField("_defaultLightsPreset", menuLightsPresetSO);
			_soloFreePlayFlowCoordinator.SetField("_defaultLightsPreset", menuLightsPresetSO);
			_soloFreePlayFlowCoordinator.SetField("_resultsLightsPreset", menuLightsPresetSO);
			_partyFreePlayFlowCoordinator.SetField("_defaultLightsPreset", menuLightsPresetSO);
			_partyFreePlayFlowCoordinator.SetField("_resultsLightsPreset", menuLightsPresetSO);
		}
	}
}