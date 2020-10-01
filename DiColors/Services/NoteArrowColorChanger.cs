using System;
using IPA.Utilities;
using UnityEngine;
using Zenject;

namespace DiColors.Services
{
	public class NoteArrowColorChanger : IInitializable, IDisposable
	{
		private static FieldAccessor<ColorNoteVisuals, float>.Accessor VisualsGlowIntensity = FieldAccessor<ColorNoteVisuals, float>.GetAccessor("_arrowGlowIntensity");
		private static FieldAccessor<ColorNoteVisuals, SpriteRenderer>.Accessor VisualsArrowSprite = FieldAccessor<ColorNoteVisuals, SpriteRenderer>.GetAccessor("_arrowGlowSpriteRenderer");
		private static FieldAccessor<ColorNoteVisuals, SpriteRenderer>.Accessor VisualsCircleSprite = FieldAccessor<ColorNoteVisuals, SpriteRenderer>.GetAccessor("_circleGlowSpriteRenderer");

		private readonly BeatmapObjectManager _beatmapObjectManager;

		public NoteArrowColorChanger(BeatmapObjectManager beatmapObjectManager)
		{
			_beatmapObjectManager = beatmapObjectManager;
		}

		public void Initialize()
		{
			_beatmapObjectManager.noteWasSpawnedEvent += NoteSpawned;
		}

		private void NoteSpawned(NoteController noteController)
		{
			if (noteController is GameNoteController gameNoteController)
			{
				ColorNoteVisuals cnv = gameNoteController.GetComponent<ColorNoteVisuals>();
				Color dynamicColor = gameNoteController.noteData.noteType == NoteType.NoteA ? Color.yellow : Color.magenta;
				float intensity = VisualsGlowIntensity(ref cnv);
				VisualsArrowSprite(ref cnv).color = dynamicColor.ColorWithAlpha(intensity);
				VisualsCircleSprite(ref cnv).color = dynamicColor;
			}
		}

		public void Dispose()
		{
			_beatmapObjectManager.noteWasSpawnedEvent -= NoteSpawned;
		}
	}
}