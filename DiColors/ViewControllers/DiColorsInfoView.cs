using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using TMPro;
using UnityEngine;
using Zenject;
using Version = Hive.Versioning.Version;

namespace DiColors.ViewControllers
{
	[ViewDefinition("DiColors.Views.info-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\Views\info-view.bsml")]
    public class DiColorsInfoView : BSMLAutomaticViewController
    {
        [UIComponent("version-text")]
        protected TextMeshProUGUI _versionText;

        [UIComponent("secondary-text")]
        protected TextMeshProUGUI _secondaryText;

        private string _version = "v0.0.0";
        [UIValue("version")]
        internal string Version
        {
            get => _version;
            set
            {
                _version = value;
                NotifyPropertyChanged();
            }
        }

        private string _secondary = "by Auros";
        [UIValue("secondary")]
        internal string Secondary
        {
            get => _secondary;
            set
            {
                _secondary = value;
                NotifyPropertyChanged();
            }
        }

        private bool _updateAvailable;
        internal bool UpdateAvailable
        {
            get => _updateAvailable;
            set
            {
                _updateAvailable = value;
                if (_updateAvailable)
                {
                    _versionText.color = _secondaryText.color = Color.white;
                }
                else
                {
                    _versionText.color = new Color(1f, 0.89f, 0.89f);
                    _secondaryText.color = new Color(1f, 1f, 0.349f);
                }
            }
        }

        [Inject]
        public void Construct([Inject(Id = "DiColors.Version")] Version version)
        {
            Version = $"v{version}";
        }

        [UIAction("secondary-clicked")]
        protected void SecondaryClicked()
        {
            var url = UpdateAvailable ? Constants.LATESTRELEASE : Constants.AUROSDEV;
            Application.OpenURL(url);
        }
		[UIAction("secondary-upd-clicked")]
		protected void SecondaryUpdClicked()
		{
			var url = UpdateAvailable ? Constants.LATESTRELEASE : Constants.HERMANESTUPD;
			Application.OpenURL(url);
		}
    }
}