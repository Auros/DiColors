using SiraUtil.Interfaces;
using Tweening;
using UnityEngine;
using Zenject;

namespace DiColors.Coloring;

internal abstract class Colorizer : IColorable
{
    public abstract string Name { get; }
    public abstract Color Color { get; set; }

    [Inject]
    protected readonly Config _config = null!;

    [Inject]
    protected readonly TimeTweeningManager _tweeningManager = null!;
}