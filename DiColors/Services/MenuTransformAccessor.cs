using UnityEngine;

namespace DiColors.Services;

internal class MenuTransformAccessor
{
    public Transform Logo { get; }
    public Transform DefaultMenuEnvironment { get; }

    public MenuTransformAccessor(MenuEnvironmentManager menuEnvironmentManager)
    {
        var memTransform = menuEnvironmentManager.transform;
        DefaultMenuEnvironment = memTransform.Find("DefaultMenuEnvironment");
        Logo = DefaultMenuEnvironment.Find("Logo");
    }
}