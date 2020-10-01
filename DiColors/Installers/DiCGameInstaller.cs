using System;
using Zenject;
using DiColors.Services;

namespace DiColors.Installers
{
	public class DiCGameInstaller : Installer
	{
		public override void InstallBindings()
		{
			Container.Bind(typeof(IInitializable), typeof(IDisposable)).To<NoteArrowColorChanger>().AsSingle();
		}
	}
}