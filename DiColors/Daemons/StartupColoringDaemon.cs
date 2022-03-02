using DiColors.Coloring;
using System.Collections.Generic;
using Zenject;

namespace DiColors.Daemons;

internal class StartupColoringDaemon : IInitializable
{
    private readonly List<Colorizer> _colorizers;

    public StartupColoringDaemon(List<Colorizer> colorizers)
    {
        _colorizers = colorizers;
    }

    public void Initialize()
    {
        foreach (var colorizer in _colorizers)
            colorizer.Color = colorizer.Color;
    }
}