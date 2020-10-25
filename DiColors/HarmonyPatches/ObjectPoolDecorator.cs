using Zenject;
using System.IO;
using HarmonyLib;
using UnityEngine;
using IPA.Utilities;

namespace DiColors.HarmonyPatches
{
	[HarmonyPatch(typeof(BeatmapObjectsInstaller), "InstallBindings")]
	internal class ObjectPoolDecorator
	{
		internal static void Prefix(BeatmapObjectsInstaller __instance, NoteController ____normalBasicNotePrefab)
		{
			var cnv = ____normalBasicNotePrefab.GetComponent<ColorNoteVisuals>();

			var container = __instance.GetProperty<DiContainer, MonoInstallerBase>("Container");
			var gameConfig = container.Resolve<Config.Game>();
			if (gameConfig.Enabled && !string.IsNullOrEmpty(gameConfig.ArrowTexture) && gameConfig.ArrowTexture != "Default" && File.Exists(Path.Combine(Constants.TEXTUREDIR, gameConfig.ArrowTexture)))
			{
				var spriteRenderer = cnv.GetField<SpriteRenderer, ColorNoteVisuals>("_arrowGlowSpriteRenderer"); //new Vector2(0.5f, 0.5f), 460, 0, SpriteMeshType.Tight
				if (File.Exists(Path.Combine(Constants.TEXTUREDIR, gameConfig.ArrowTexture)))
				{
					var bytes = File.ReadAllBytes(Path.Combine(Constants.TEXTUREDIR, gameConfig.ArrowTexture));
					var tex = new Texture2D(2, 2);
					tex.LoadImage(bytes);
					var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 460, 0, SpriteMeshType.Tight);
					spriteRenderer.sprite = sprite;
				}
			}
		}
	}
}