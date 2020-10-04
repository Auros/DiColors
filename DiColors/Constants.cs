using System.IO;
using IPA.Utilities;

namespace DiColors
{
	internal static class Constants
	{
		internal const string AUROSDEV = "https://donate.auros.dev";
		internal const string LATESTRELEASE = "https://github.com/Auros/DiColors/releases/latest";
		internal static readonly string FOLDERDIR = Path.Combine(UnityGame.UserDataPath, "DiColors");
		internal static readonly string PROFILEDIR = Path.Combine(FOLDERDIR, "Profiles");
	}
}