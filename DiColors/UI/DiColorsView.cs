using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
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

    public class ColorBox
    {
        public Color Color { get; set; }

        public ColorBox(Color color)
        {
            Color = color;
        }
    }

    protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
    {
        List<Color> colors = new()
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.magenta,
            Color.black,
            Color.yellow,
            Color.white
        };

        for (int i = 0; i < colors.Count; i++)
        {
            ColorBox cbA = new(colors[i]);
            ColorProfileOption co = new("C_" + i, () => cbA.Color, c => cbA.Color = c);
            Data.Add(co);

            if (i == 0)
                _activeOption = co;
        }

        _activeColor = _activeOption.Color;

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