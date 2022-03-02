using DiColors.Coloring;
using SiraUtil.Attributes;
using SiraUtil.Zenject;
using Zenject;

namespace DiColors;

[Bind(Location.Menu)]
internal class TestColor : IInitializable
{
    private readonly TopSignColorizer _colorizer;

    public TestColor(TopSignColorizer colorizer)
    {
        _colorizer = colorizer;
    }

    public void Initialize()
    {
        _colorizer.Color = new(0, 1, 0, 1f);
    }
}