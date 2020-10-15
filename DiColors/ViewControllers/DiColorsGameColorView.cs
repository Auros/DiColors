using Zenject;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Components.Settings;

namespace DiColors.ViewControllers
{
	[HotReload(RelativePathToLayout = @"..\Views\game-color-view.bsml")]
	public class DiColorsGameColorView : BSMLAutomaticViewController
	{
		private Config.Game _gameConfig;
		private Config.Game _stashedConfig;
		private CachedMediaAsyncLoader _mediaLoader;
		private CancellationTokenSource _cancellationToken;

		[UIValue("textures")]
		public List<object> textureOptions = new List<object>();

		[UIComponent("preview")]
		protected Image preview;

		[UIParams]
		protected BSMLParserParams parserParams;

		[UIComponent("dropdown")]
		protected DropDownListSetting dropdown;

		[UIValue("left-color")]
		public Color LeftColor
		{
			get => _gameConfig.LeftArrowColor;
			set => _gameConfig.LeftArrowColor = value;
		}

		[UIValue("right-color")]
		public Color RightColor
		{
			get => _gameConfig.RightArrowColor;
			set => _gameConfig.RightArrowColor = value;
		}

		[UIValue("left-enabled")]
		public bool LeftEnabled
		{
			get => _gameConfig.UseLeftColor;
			set => _gameConfig.UseLeftColor = value;
		}

		[UIValue("right-enabled")]
		public bool RightEnabled
		{
			get => _gameConfig.UseRightColor;
			set => _gameConfig.UseRightColor = value;
		}

		[UIValue("texture")]
		public string Texture
		{
			get => File.Exists(Path.Combine(Constants.TEXTUREDIR, _gameConfig.ArrowTexture)) ? new FileInfo(Path.Combine(Constants.TEXTUREDIR, _gameConfig.ArrowTexture)).Name : "Default";
			set => _gameConfig.ArrowTexture = value;
		}

		[Inject]
		public async void Construct(Config.Game gameConfig, CachedMediaAsyncLoader mediaLoader)
		{
			_gameConfig = gameConfig;
			_mediaLoader = mediaLoader;
			_stashedConfig = _gameConfig.Copy();
			
			Directory.CreateDirectory(Constants.FOLDERDIR);
			Directory.CreateDirectory(Constants.TEXTUREDIR);

			Reload();

			// Lazily load the new texture
			await Task.Run(() => Thread.Sleep(2000));
			await _mediaLoader.LoadSpriteAsync(Path.Combine(Constants.TEXTUREDIR, _gameConfig.ArrowTexture), CancellationToken.None);
		}

		[UIAction("#post-parse")]
		protected void Parsed()
		{
			LoadImage(_gameConfig.ArrowTexture);
		}

		[UIAction("load-image")]
		protected async void LoadImage(string name)
		{
			if (!string.IsNullOrEmpty(name) && name != "Default" && File.Exists(Path.Combine(Constants.TEXTUREDIR, name)))
			{
				try
				{
					Sprite texture = await _mediaLoader.LoadSpriteAsync(Path.Combine(Constants.TEXTUREDIR, name), CancellationToken.None);
					
					preview.gameObject.SetActive(true);
					preview.sprite = texture;
				}
				catch { }
			}
			else
			{
				preview.gameObject.SetActive(false);
			}
		}

		[UIAction("on-reset")]
		protected void Reset()
		{
			Apply(_gameConfig, _stashedConfig);
			parserParams.EmitEvent("get");
			Reload();
		}

		[UIAction("on-apply")]
		protected void Apply()
		{
			parserParams.EmitEvent("apply");
			_stashedConfig = _gameConfig.Copy();
			Reload();
		}

		[UIAction("reload")]
		protected void Reload()
		{
			textureOptions.Clear();
			var directory = new DirectoryInfo(Constants.TEXTUREDIR);
			var files = directory.EnumerateFiles().Where(x => x.Extension.EndsWith("png") || x.Extension.EndsWith("jpg") || x.Extension.EndsWith("jpeg"));
			textureOptions.Add("Default");
			for (int i = 0; i < files.Count(); i++)
			{
				var file = files.ElementAt(i);
				textureOptions.Add(file.Name);
			}
			if (dropdown != null)
			{
				dropdown.values = textureOptions;
				dropdown?.UpdateChoices();
			}
		}

		private void Apply(Config.Game toApplyTo, Config.Game donor)
		{
			toApplyTo.Enabled = donor.Enabled;
			toApplyTo.ArrowTexture = donor.ArrowTexture;
			toApplyTo.UseLeftColor = donor.UseLeftColor;
			toApplyTo.UseRightColor = donor.UseRightColor;
			toApplyTo.LeftArrowColor = donor.LeftArrowColor;
			toApplyTo.RightArrowColor = donor.RightArrowColor;
		}

		protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
			base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);
			_cancellationToken = new CancellationTokenSource();
		}

		protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
		{
			base.DidDeactivate(removedFromHierarchy, screenSystemDisabling);
			_cancellationToken?.Cancel();
			parserParams?.EmitEvent("hide-modal");
		}
	}
}