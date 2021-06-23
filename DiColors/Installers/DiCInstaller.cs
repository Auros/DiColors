using SemVer;
using Zenject;
using SiraUtil.Interfaces;

namespace DiColors.Installers
{
    public class DiCInstaller : Installer<Config, Version, DiCInstaller>
    {
        private readonly Config _config;
        private readonly Version _version;

        public DiCInstaller(Config config, Version version)
        {
            _config = config;
            _version = version;
        }

        public override void InstallBindings()
        {
			_config.MenuSettings.Config = _config;
			_config.GameSettings.Config = _config;

            Container.Bind<Config>().FromInstance(_config).AsSingle();
            Container.Bind<Config.Menu>().FromInstance(_config.MenuSettings).AsSingle();
            Container.Bind<Config.Game>().FromInstance(_config.GameSettings).AsSingle();
            Container.Bind<Version>().WithId("DiColors.Version").FromInstance(_version).AsCached();
        }
    }
}