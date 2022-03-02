using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using DiColors.Coloring;
using SiraUtil.Logging;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DiColors.UI;

[ViewDefinition("DiColors.UI.Views.dicolors-view.bsml"), HotReload(RelativePathToLayout = @"Views/dicolors-view.bsml")]
internal class DiColorsView : BSMLAutomaticViewController
{
    private Color _activeColor = Color.white;
    private ColorProfileOption _activeOption = null!;

    [Inject]
    protected readonly SiraLog _siraLog = null!;

    [Inject]
    protected readonly List<Colorizer> _colorizers = null!;

    [UIValue("color-profile-options")]
    public List<object> Data { get; } = new();

    [UIValue("active-option")]
    public ColorProfileOption ActiveOption
    {
        get => _activeOption;
        set
        {
            _activeOption = value;
            NotifyPropertyChanged(nameof(ActiveOption));
            ActiveColor = _activeOption.Color;
        }
    }

    [UIValue("active-color")]
    public Color ActiveColor
    {
        get => _activeColor;
        set
        {
            _activeColor = value;
            NotifyPropertyChanged(nameof(ActiveColor));
            _activeOption.Apply(_activeColor);
        }
    }

    protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
    {
        if (firstActivation)
            foreach (var colorizer in _colorizers)
                Data.Add(new ColorProfileOption(colorizer.Name, () => colorizer.Color, c => colorizer.Color = c));

        _activeOption = (Data[0] as ColorProfileOption)!;

        base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);
    }

    public class ColorProfileOption
    {
        public Color Color => _getColor.Invoke();

        private readonly string _name;
        private readonly Func<Color> _getColor;
        private readonly Action<Color> _setColor;

        public ColorProfileOption(string name, Func<Color> getColor, Action<Color> setColor)
        {
            _name = name;
            _getColor = getColor;
            _setColor = setColor;
        }

        public void Apply(Color color)
        {
            _setColor.Invoke(color);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}