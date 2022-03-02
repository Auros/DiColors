using SiraUtil.Interfaces;
using UnityEngine;

namespace DiColors.Coloring;

internal abstract class Colorizer : IColorable
{
    public abstract string Name { get; }
    public abstract Color Color { get; set; }
}