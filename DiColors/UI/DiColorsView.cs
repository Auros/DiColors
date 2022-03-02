using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace DiColors.UI;

[ViewDefinition("DiColors.UI.Views.dicolors-view.bsml"), HotReload(RelativePathToLayout = @"Views/dicolors-view.bsml")]
internal class DiColorsView : BSMLAutomaticViewController
{

}