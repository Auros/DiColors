using Zenject;

namespace DiColors.Installers
{
	public class DiCInstaller : Installer<Config, DiCInstaller>
	{
		private readonly Config _config;

		public DiCInstaller(Config config)
		{
			_config = config;
		}

		public override void InstallBindings()
		{
			Container.Bind<Config.Menu>().FromInstance(_config.MenuSettings).AsSingle();
			Container.Bind<Config.Game>().FromInstance(_config.GameSettings).AsSingle();
		}
	}
}