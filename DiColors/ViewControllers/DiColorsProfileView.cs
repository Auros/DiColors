using Zenject;
using DiColors.Services;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;

namespace DiColors.ViewControllers
{
	/*[HotReload(RelativePathToLayout = @"..\Views\profile-view.bsml")]
	public class DiColorsProfileView : BSMLAutomaticViewController
	{
		private Config _currentConfig;
		private ProfileManager _profileManager;

		[UIComponent("profile-list")]
		public CustomListTableData profileListTable;

		[UIValue("loaded")]
		protected bool Loaded => !Loading;

		private bool _loading = true;
		[UIValue("loading")]
		private bool Loading
		{
			get => _loading;
			set
			{
				_loading = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged("Loaded");
			}
		}

		[Inject]
		public void Construct(Config config, ProfileManager profileManager)
		{
			_currentConfig = config;
			_profileManager = profileManager;
		}

		protected override async void DidActivate(bool firstActivation, ActivationType type)
		{
			base.DidActivate(firstActivation, type);

			Loading = true;
			var allProfiles = await _profileManager.GetAllProfiles();
			profileListTable.tableView.ClearSelection();
			profileListTable.data.Clear();
			profileListTable.data.Add(new CustomListTableData.CustomCellInfo(_currentConfig.Name));
			foreach (var prof in allProfiles)
			{
				profileListTable.data.Add(new CustomListTableData.CustomCellInfo(prof.Name));
			}
			profileListTable.tableView.ReloadData();
			await System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(3000));
			Loading = false;
		}
	}*/
}
