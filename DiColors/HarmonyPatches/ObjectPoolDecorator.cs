using HarmonyLib;

namespace DiColors.HarmonyPatches
{
	[HarmonyPatch(typeof(GameplayCoreBeatmapObjectPoolsInstaller), "InstallBindings")]
	internal class ObjectPoolDecorator
	{
		internal static void Prefix(ref GameplayCoreBeatmapObjectPoolsInstaller __instance, ref NoteController ____normalBasicNotePrefab)
		{
			var cnv = ____normalBasicNotePrefab.GetComponent<ColorNoteVisuals>();
			/*
			var spriteRenderer = cnv.GetField<SpriteRenderer, ColorNoteVisuals>("_arrowGlowSpriteRenderer");
			var tex = BeatSaberMarkupLanguage.Utilities.FindTextureInAssembly("DiColors.Resources.ArrowGlow2x.png");
			var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 460, 0, SpriteMeshType.Tight);
			spriteRenderer.sprite = sprite;*/
		}
	}
}