using System;
using Zenject;
using UnityEngine;
using IPA.Utilities;

namespace DiColors.Services
{
	public class NoteArrowColorChanger : IInitializable, IDisposable
	{
		private static readonly FieldAccessor<ColorNoteVisuals, float>.Accessor VisualsGlowIntensity = FieldAccessor<ColorNoteVisuals, float>.GetAccessor("_arrowGlowIntensity");
		private static readonly FieldAccessor<ColorNoteVisuals, MeshRenderer>.Accessor VisualsArrowMesh = FieldAccessor<ColorNoteVisuals, MeshRenderer>.GetAccessor("_arrowMeshRenderer");
		private static readonly FieldAccessor<ColorNoteVisuals, SpriteRenderer>.Accessor VisualsArrowSprite = FieldAccessor<ColorNoteVisuals, SpriteRenderer>.GetAccessor("_arrowGlowSpriteRenderer");
		private static readonly FieldAccessor<ColorNoteVisuals, SpriteRenderer>.Accessor VisualsCircleSprite = FieldAccessor<ColorNoteVisuals, SpriteRenderer>.GetAccessor("_circleGlowSpriteRenderer");

		private readonly Config.Game _gameConfig;
		private readonly BeatmapObjectManager _beatmapObjectManager;

		public NoteArrowColorChanger(Config.Game gameConfig, BeatmapObjectManager beatmapObjectManager)
		{
			_gameConfig = gameConfig;
			_beatmapObjectManager = beatmapObjectManager;
		}

		public void Initialize()
		{
			_beatmapObjectManager.noteWasSpawnedEvent += NoteSpawned;
		}

		private void NoteSpawned(NoteController noteController)
		{
			if (!_gameConfig.Enabled || (_gameConfig.UseLeftColor == false && _gameConfig.UseRightColor == false))
			{
				return;
			}
			if (noteController is GameNoteController gameNoteController)
			{
				ColorNoteVisuals cnv = gameNoteController.GetComponent<ColorNoteVisuals>();
				
				if (gameNoteController.noteData.noteType == NoteType.NoteA && !_gameConfig.UseLeftColor)
				{
					return;
				}
				if (gameNoteController.noteData.noteType == NoteType.NoteB && !_gameConfig.UseRightColor)
				{
					return;
				}
				Color dynamicColor = gameNoteController.noteData.noteType == NoteType.NoteA ? _gameConfig.LeftArrowColor : _gameConfig.RightArrowColor;
				float intensity = VisualsGlowIntensity(ref cnv);
				VisualsArrowSprite(ref cnv).color = dynamicColor.ColorWithAlpha(intensity);
				VisualsCircleSprite(ref cnv).color = dynamicColor;
				VisualsArrowMesh(ref cnv).material.color = dynamicColor;
			}
		}

		public void Dispose()
		{
			_beatmapObjectManager.noteWasSpawnedEvent -= NoteSpawned;
		}
	}
}