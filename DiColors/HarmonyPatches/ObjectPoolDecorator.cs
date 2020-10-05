using Zenject;
using System.IO;
using HarmonyLib;
using UnityEngine;
using IPA.Utilities;
using System.Threading;
using DiColors.Services;

namespace DiColors.HarmonyPatches
{
	[HarmonyPatch(typeof(GameplayCoreBeatmapObjectPoolsInstaller), "InstallBindings")]
	internal class ObjectPoolDecorator
	{
		internal static async void Prefix(GameplayCoreBeatmapObjectPoolsInstaller __instance, NoteController ____normalBasicNotePrefab)
		{
			var cnv = ____normalBasicNotePrefab.GetComponent<ColorNoteVisuals>();

			var container = __instance.GetProperty<DiContainer, MonoInstallerBase>("Container");
			var gameConfig = container.Resolve<Config.Game>();
			if (!string.IsNullOrEmpty(gameConfig.ArrowTexture) && gameConfig.ArrowTexture != "Default" && File.Exists(Path.Combine(Constants.TEXTUREDIR, gameConfig.ArrowTexture)))
			{
				var mediaLoader = container.Resolve<CachedSpriteMediaAsyncLoader>();

				var spriteRenderer = cnv.GetField<SpriteRenderer, ColorNoteVisuals>("_arrowGlowSpriteRenderer");
				var tex = await mediaLoader.LoadSpriteAsync(Path.Combine(Constants.TEXTUREDIR, gameConfig.ArrowTexture), CancellationToken.None, new Vector2(0.5f, 0.5f), 460, 0, SpriteMeshType.Tight);
				var sprite = tex;
				spriteRenderer.sprite = sprite;
			}
		}
	}
}