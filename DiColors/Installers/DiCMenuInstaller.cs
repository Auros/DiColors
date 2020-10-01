using Zenject;
using DiColors.Services;

namespace DiColors.Installers
{
	public class DiCMenuInstaller : Installer
	{
		public override void InstallBindings()
		{
			Container.Bind(typeof(IInitializable)).To<MenuColorSwapper>().AsSingle();
		}
	}
}