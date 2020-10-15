using System;
using Zenject;
using UnityEngine;
using IPA.Utilities;
using System.Runtime.CompilerServices;

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
		private readonly ConditionalWeakTable<GameNoteController, ColorNoteVisuals> _colorTable = new ConditionalWeakTable<GameNoteController, ColorNoteVisuals>();

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
				if (!_colorTable.TryGetValue(gameNoteController, out ColorNoteVisuals cnv))
				{
					cnv = gameNoteController.GetComponent<ColorNoteVisuals>();
					_colorTable.Add(gameNoteController, cnv);
				}
				if (gameNoteController.noteData.colorType == ColorType.ColorA && !_gameConfig.UseLeftColor)
				{
					return;
				}
				if (gameNoteController.noteData.colorType == ColorType.ColorB && !_gameConfig.UseRightColor)
				{
					return;
				}
				Color dynamicColor = gameNoteController.noteData.colorType == ColorType.ColorA ? _gameConfig.LeftArrowColor : _gameConfig.RightArrowColor;
				float intensity = VisualsGlowIntensity(ref cnv);
				VisualsArrowSprite(ref cnv).color = dynamicColor.ColorWithAlpha(intensity);
				VisualsCircleSprite(ref cnv).color = dynamicColor.ColorWithAlpha(intensity);
			}
		}

		public void Dispose()
		{
			_beatmapObjectManager.noteWasSpawnedEvent -= NoteSpawned;
		}
	}
}